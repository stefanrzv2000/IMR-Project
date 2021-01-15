using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class OnBoardDragon : OnBoardDestructible
{
    //Internal Use
    public int SpeedRemained;
    public int Speed;
    public int Attack;
    public int Range;
    
    public bool CanAttack;
    public bool CanRetaliate;

    //External Use
    public int Type;
    public int Race;

    public DragonGenerator DragonGenerator;

    private DragonStatus DragonHealth;
    private DragonStatus DragonAttack;

    public OnBoardDragon(Vector2Int targetPosition, Board board, CardDragon cardDragon)
    {
        DragonGenerator = GameObject.Find("DragonGenerator").GetComponent<DragonGenerator>();

        Board = board;
        DestructibleType = DRAGON;

        BoardY = targetPosition.y;
        BoardX = targetPosition.x;
        ProjectY = BoardY;
        ProjectX = BoardX;


        Health    = cardDragon.MaxHealth;
        MaxHealth = cardDragon.MaxHealth;
        Speed     = cardDragon.Speed;
        Attack    = cardDragon.Attack;
        Range     = cardDragon.Range;
        Name      = cardDragon.Name;
        Type      = cardDragon.Type;
        Race      = cardDragon.Race;
        Owner     = cardDragon.Owner;

        SpeedRemained = 0;
        CanAttack = false;
        CanRetaliate = true;
        Alive = true;

        Board.Destructables[BoardY, BoardX] = this;
        
    }

    public void ResetTurn()
    {
        SpeedRemained = Speed;
        CanAttack = true;
        CanRetaliate = true;
    }

    private List<Tuple<Vector2Int, int>> getPositionsInfoList()
    {
        List<Tuple<Vector2Int, int>> result = new List<Tuple<Vector2Int, int>>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        int[,] dist = new int[Board.Height, Board.Width];
        for (int y = 0; y < Board.Height; y++)
        {
            for (int x = 0; x < Board.Height; x++)
            {
                dist[y, x] = -1;
            }
        }

        queue.Enqueue(new Vector2Int(BoardX, BoardY));
        dist[BoardY, BoardX] = 0;

        List<int> dirX = new List<int>() { 0, 1, 0, -1 };
        List<int> dirY = new List<int>() { 1, 0, -1, 0 };

        while (queue.Count > 0)
        {
            Vector2Int cur = queue.Peek();
            queue.Dequeue();
            result.Add(new Tuple<Vector2Int, int>(cur, dist[cur.y, cur.x]));

            for (int i = 0; i < 4; i++)
            {
                int ny = cur.y + dirY[i];
                int nx = cur.x + dirX[i];

                if (ny < 0 || ny >= Board.Height ||
                    nx < 0 || nx >= Board.Width ||
                    Board.Destructables[ny, nx] != null ||
                    dist[ny, nx] != -1)
                    continue;

                dist[ny, nx] = dist[cur.y, cur.x] + 1;
                if (dist[ny, nx] <= SpeedRemained)
                {
                    Vector2Int toAdd = new Vector2Int(nx, ny);
                    queue.Enqueue(toAdd);
                }
            }
        }

        return result;
    }

    public List<Vector2Int> GetMovingPositions()
    {
        Debug.Log("Moving Positions Call 1");
        var possibilities = getPositionsInfoList();
        Debug.Log("Moving Positions Call 2");
        List<Vector2Int> result = new List<Vector2Int>();
        Debug.Log("Moving Positions Call 3");
        foreach (var info in possibilities)
        {
            var position = info.Item1;
            result.Add(position);
        }
        Debug.Log("Moving Positions Call 4");

        return result;
    }

    private int GetMovingCost(Vector2Int targetPosition)
    {
        var possibilities = getPositionsInfoList();
        //List<Vector2Int> result = new List<Vector2Int>();
        foreach (var info in possibilities)
        {
            var position = info.Item1;
            if (targetPosition == position)
                return info.Item2;
        }

        return 0;
    }

    public List<Vector2Int> GetAttackingPositions()
    {
        var result = new List<Vector2Int>();

        if (!CanAttack) return result;

        for (int y = 0; y < Board.Height; y++)
        {
            for (int x = 0; x < Board.Width; x++)
            {
                if (Board.Destructables[y, x] != null &&
                    this.DistanceTo(new Vector2Int(x, y)) <= Range &&
                    Board.Destructables[y, x].DestructibleType != OCCUPIED &&
                    Board.Destructables[y, x].Owner != Owner
                    )
                {
                    result.Add(new Vector2Int(x, y));
                }
            }
        }
        return result;
    }

    public void MoveOn(Vector2Int targetPosition)
    {
        SpeedRemained -= GetMovingCost(targetPosition);

        Board.Destructables[BoardY, BoardX] = null;

        BoardY = targetPosition.y;
        BoardX = targetPosition.x;

        ProjectY = BoardY;
        ProjectX = BoardX;

        Board.Destructables[BoardY, BoardX] = this;
    }

    public void AttackOn(Vector2Int targetPosition)
    {
        if (!CanAttack) return;
        int y = targetPosition.y;
        int x = targetPosition.x;
        
        OnBoardDestructible attackedDestructible = Board.Destructables[y, x];

        Debug.Log("first attack");
        attackedDestructible.ReceiveDamage(Attack);
        Debug.Log("first attack");
        CanAttack = false;

        if (x == 0 || x == Board.Width-1 || y == 0 || y == Board.Height-1)
            return;

        OnBoardDragon attackedDragon = (OnBoardDragon) attackedDestructible;

        if (attackedDragon.Alive && attackedDragon.Range >= attackedDragon.DistanceTo(this)
                                 && attackedDragon.CanRetaliate)
        {
            Debug.Log("second attack");
            this.ReceiveDamage(attackedDragon.Attack);
            attackedDragon.CanRetaliate = false;
        }
    }

    public int DistanceTo(OnBoardDestructible destructible)
    {
        return Math.Abs(destructible.BoardY - this.BoardY) + Math.Abs(destructible.BoardX - this.BoardX);
    }

    public int DistanceTo(Vector2Int targetPosition)
    {
        return Math.Abs(targetPosition.y - BoardY) + Math.Abs(targetPosition.x - BoardX);
    }

    public void ChangeOwner()
    {
        Owner = 3 - Owner;
        PhysicInstance.transform.Rotate(Vector3.up, 180);
        UpdateOnBoard();
    }

    public void UpdateOnBoard()
    {
        if (Alive)
        {
            if (PhysicInstance == null)
            {
                PhysicInstance = DragonGenerator.CreateDragon(BoardX + 8 * BoardY, Race, Type, Owner);
                PhysicInstance.transform.Find("HealthStatus").gameObject.GetComponent<DragonStatus>().UpdateStatus(Health);
                PhysicInstance.transform.Find("AttackStatus").gameObject.GetComponent<DragonStatus>().UpdateStatus(Attack);
            }

            else if (PhysicInstance != null)
            {
                var parent = GameObject.Find("GameTable").transform.GetChild(BoardX + 8 * BoardY);
                float multiplier = Owner == 1 ? -1 : 1;
                PhysicInstance.transform.parent = parent;
                PhysicInstance.transform.localPosition = new Vector3(multiplier * 0.39f, 0.036f, multiplier * 0.66f);

                PhysicInstance.transform.Find("HealthStatus").gameObject.GetComponent<DragonStatus>().UpdateStatus(Health);
                PhysicInstance.transform.Find("AttackStatus").gameObject.GetComponent<DragonStatus>().UpdateStatus(Attack);
            }
        }
        else
        {
            if (PhysicInstance == null)
            {
                // It was already dead
            }

            else if (PhysicInstance != null)
            {
                // It just died
                //GameObject.Destroy(PhysicInstance);
            }
        }
    }

    public override void UpdateStatus()
    {
        if (PhysicInstance != null)
        {
            PhysicInstance.transform.Find("HealthStatus").gameObject.GetComponent<DragonStatus>().UpdateStatus(Health);
            PhysicInstance.transform.Find("AttackStatus").gameObject.GetComponent<DragonStatus>().UpdateStatus(Attack);
        }
    }

    public int MinZero(int x)
    {
        return x < 0 ? 0 : x;
    }

    public void ReceiveBuff(int health, int attack, int range, int speed, bool star, string textUpdate)
    {
        bool negative = health + attack + range + speed < 0;
        
        MaxHealth += health;
        Health += health;
        Attack += attack;
        Range += range;
        Speed += speed;
        SpeedRemained += speed;

        Attack = MinZero(Attack);
        Range = MinZero(Range);
        Speed = MinZero(Speed);
        SpeedRemained = MinZero(SpeedRemained);

        StatusUpdateType status = StatusUpdateType.HEALTH;
        if (star)
        {
            status = StatusUpdateType.STAR;
        }
        else if(attack != 0)
        {
            status = StatusUpdateType.ATTACK;
        }

        Color color = negative ? Color.red : Color.green;

        PhysicInstance.transform.Find("ActionStats").gameObject.GetComponent<ActionStats>().ShowUpdate(status, textUpdate, color);
        UpdateStatus();
    }
}
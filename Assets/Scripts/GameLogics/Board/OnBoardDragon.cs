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

    public GameObject PhysicalDragon;
    public DragonGenerator DragonGenerator;

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

        PhysicalDragon = DragonGenerator.CreateDragon(BoardX * 8 + BoardY, Race, Type, Owner);

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
                if (dist[ny, nx] < SpeedRemained)
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
        var possibilities = getPositionsInfoList();
        List<Vector2Int> result = new List<Vector2Int>();
        foreach (var info in possibilities)
        {
            var position = info.Item1;
            result.Add(position);
        }

        return result;
    }

    private int GetMovingCost(Vector2Int targetPosition)
    {
        var possibilities = getPositionsInfoList();
        List<Vector2Int> result = new List<Vector2Int>();
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

        for (int y = 0; y < Board.Height; y++)
        {
            for (int x = 0; x < Board.Width; x++)
            {
                if (Board.Destructables[y, x] != null &&
                    this.DistanceTo(Board.Destructables[y, x]) <= Range &&
                    Board.Destructables[y, x].DestructibleType != OCCUPIED)
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
        int y = targetPosition.y;
        int x = targetPosition.x;
        
        OnBoardDestructible attackedDestructible = Board.Destructables[y, x];

        attackedDestructible.ReceiveDamage(Attack);
        CanAttack = false;

        if (x == 0 || x == Board.Width-1 || y == 0 || y == Board.Height-1)
            return;

        OnBoardDragon attackedDragon = (OnBoardDragon) attackedDestructible;

        if (attackedDragon.Alive && attackedDragon.Range >= attackedDragon.DistanceTo(this)
                                 && attackedDragon.CanRetaliate)
        {
            this.ReceiveDamage(attackedDragon.Attack);
            attackedDragon.CanRetaliate = false;
        }
    }

    public int DistanceTo(OnBoardDestructible destructible)
    {
        return destructible.BoardY - this.BoardY + destructible.BoardX - this.BoardX;
    }

    public int DistanceTo(Vector2Int targetPosition)
    {
        return targetPosition.y - BoardY + targetPosition.x - BoardX;
    }
}
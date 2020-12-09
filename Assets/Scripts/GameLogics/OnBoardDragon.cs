using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class OnBoardDragon : MonoBehaviour
{
    //Internal Use
    public int BoardX;
    public int BoardY;

    public int MaxHealth;
    public int Health;
    public int SpeedRemained;
    public int Speed;
    public int Attack;
    public int Range;
    
    public bool CanAttack;
    public bool CanRetaliate;
    public bool Alive;

    //External Use
    public string Name;

    public int Type;
    public int Race;
    public int Owner;
    
    public Board Board;

    public OnBoardDragon(Vector2Int targetPosition, Board board, CardDragon cardDragon)
    {
        BoardY = targetPosition.y;
        BoardX = targetPosition.x;

        Board = board;

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

        Board.dragons[BoardY, BoardX] = this;
    }

    public void ResetTurn()
    {
        SpeedRemained = Speed;
        CanAttack = true;
        CanRetaliate = true;
    }

    public void MoveOn(Vector2Int targetPosition)
    {
        Board.dragons[BoardY, BoardX] = null;
        SpeedRemained -= DistanceTo(targetPosition);

        BoardY = targetPosition.y;
        BoardX = targetPosition.x;
        Board.dragons[BoardY, BoardX] = this;
    }

    public void ReceiveDamage(int attack)
    {
        Health -= attack;
        if (Health > 0)
            return;

        Board.dragons[BoardY, BoardX] = null;
        Alive = false;
        Destroy(this);
    }

    public void AttackOn(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;
        OnBoardDragon attackedDragon = Board.dragons[y, x];

        CanAttack = false;

        attackedDragon.ReceiveDamage(Attack);
        if (attackedDragon.Alive && attackedDragon.Range >= attackedDragon.DistanceTo(this)
                                 && attackedDragon.CanRetaliate)
        {
            this.ReceiveDamage(attackedDragon.Attack);
            attackedDragon.CanRetaliate = false;
        }
    }

    public int DistanceTo(OnBoardDragon dragon)
    {
        return dragon.BoardY - this.BoardY + dragon.BoardX - this.BoardX;
    }

    public int DistanceTo(Vector2Int targetPosition)
    {
        return targetPosition.y - BoardY + targetPosition.x - BoardX;
    }
}

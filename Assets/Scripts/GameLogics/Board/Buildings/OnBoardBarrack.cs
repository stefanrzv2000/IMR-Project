using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardBarrack : OnBoardBuilding
{
    public int Tier;
    public const int MAX_UPGRADES = 1;

    public const int MAX_HP = 50;

    public OnBoardBarrack(int owner, Board board)
    {
        Board = board;
        DestructibleType = BARRACK;

        Owner = owner;

        Tier = 0;

        Alive = true;
        MaxHealth = MAX_HP;
        Health = MAX_HP;

        if (owner == 1)
        {
            ProjectY = Board.Height - 1 - 1.5f;
            ProjectX = -0.5f;
        }
        else
        {
            ProjectY = +1.5f;
            ProjectX = Board.Width - 1 + 0.5f;
        }
        if (owner == 1)
            PhysicInstance = GameObject.Find("AllyBarrack");
        else
            PhysicInstance = GameObject.Find("EnemyBarrack");
    
        
    }
    public void Upgrade()
    {
        if (Tier < MAX_UPGRADES)
            Tier += 1;
    }
    public CardDragon ResetTurn(int race)
    {
        int type = Random.Range(0, 2+Tier);
        return new CardDragon(type, race, Owner);
    }
}

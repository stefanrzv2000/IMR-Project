using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class OnBoardNest : OnBoardBuilding
{
    private const int MAX_HP = 100;
    public OnBoardNest(int owner, Board board)
    {
        Board = board;
        DestructibleType = NEST;

        Owner = owner;
        Alive = true;
        MaxHealth = MAX_HP;
        Health = MAX_HP;

        ProjectX = Board.Width / 2 - 0.5f;
        if (owner == 1)
        {
            ProjectY = Board.Height + 0.5f;
        }
        else
        {
            ProjectY = -0.5f;
        }
        if (owner==1)
            PhysicInstance = GameObject.Find("AllyNest");
        else
            PhysicInstance = GameObject.Find("EnemyNest");



    }
}

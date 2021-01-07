using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardMageTower : OnBoardBuilding
{
    public int Tier;
    public int []MaxSpellsIndex;
    public const int MAX_UPGRADES = 2;

    public const int MAX_HP = 50;

    public OnBoardMageTower(int owner, Board board)
    {
        Board = board;
        DestructibleType = MAGE_TOWER;

        Tier = 0;
        Owner = owner;

        MaxSpellsIndex = new int[MAX_UPGRADES+1];

        MaxSpellsIndex[0] = 2;
        MaxSpellsIndex[1] = 4;
        MaxSpellsIndex[2] = 5;

        Alive = true;
        MaxHealth = MAX_HP;
        Health = MAX_HP;

        if (owner == 1)
        {
            ProjectY = Board.Height - 1 - 1.5f;
            ProjectX = Board.Width - 1 + 0.5f;
        }
        else
        {
            ProjectY = +1.5f;
            ProjectX = -0.5f;
        }
    
        if (owner==1)
            PhysicInstance = GameObject.Find("AllyMageTower");
        else
            PhysicInstance = GameObject.Find("EnemyMageTower");

    }

    public void Upgrade()
    {
        if (Tier < MAX_UPGRADES)
            Tier += 1;
    }

    public CardSpell ResetTurn(int race)
    {
        int index = Random.Range(0, MaxSpellsIndex[2]); // to be modified from 2 back to Tier
        return CardSpellCreator.GenerateCardSpell(index, Owner, race);
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellEarthSteel : CardSpell
{
    private const int PermanentHealth = 1;
    private const int PermanentAttack = 2;
    private const int CastTimes = 3;

    public CardSpellEarthSteel(int owner)
    {
        Name = "Earth Steel Gear";
        Description = $"Gives {PermanentHealth} Health and {PermanentAttack} Attack to an ally dragon." +
                      $"Auto Casts {CastTimes} times";
        ID = FIRE_BOLT;
        Owner = owner;
        ManaCost = 5;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        var allyDragons = Board.GetAllDragonsOfOwner(Owner);
        for (int i = 0; i < CastTimes; i++)
        {
            if (allyDragons.Count > 0)
            {
                int index = Random.Range(0, allyDragons.Count);
                var selectedDragon = allyDragons[index];
                selectedDragon.Attack += PermanentAttack;
                selectedDragon.MaxHealth += PermanentHealth;
                selectedDragon.Health += PermanentHealth;
            }
        }
    }
}
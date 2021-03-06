﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellEarthQuake : CardSpell
{
    private const int DamageDealt = 4;
    public CardSpellEarthQuake(int owner)
    {
        Name = "Earth Quake";
        Description = $"Deals {DamageDealt} to all enemies and {DamageDealt/2} to all allies";
        Race = EARTH;
        ID = EARTH_QUAKE;
        Owner = owner;
        ManaCost = 8;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        var allDragons = Board.GetAllDragons();
        foreach (var dragon in allDragons)
        {
            if (dragon.Owner == Owner)
            {
                dragon.ReceiveDamage(DamageDealt/2);
            }
            else
            {
                dragon.ReceiveDamage(DamageDealt);
            }
        }
    }

    public override List<Vector2Int> GetAvailableTargets()
    {
        return Board.GetAllInnerPositions();
    }

    public override List<Vector2Int> GetHoverPositions(Vector2Int position)
    {
        return Board.GetAllInnerPositions();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellAirTornado : CardSpell
{
    private const int DamageDealt = 6;

    public CardSpellAirTornado(int owner)
    {
        Name = "Air Tornado";
        Description = $"Deals {DamageDealt} damage to all the dragons from selected and adjacent rows";
        ID = AIR_TORNADO;
        Owner = owner;
        ManaCost = 8;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        var allDragons = Board.GetAllDragons();
        foreach (var dragon in allDragons)
        {
            if (Math.Abs(dragon.BoardY - y) <= 1)
                dragon.ReceiveDamage(DamageDealt);
        }
    }
}

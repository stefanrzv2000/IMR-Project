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
        Race = AIR;
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

    public override List<Vector2Int> GetAvailableTargets()
    {
        List<Vector2Int> result = new List<Vector2Int>();
        for (int y = 2; y < Board.Height - 2; y++)
        {
            for (int x = 1; x < Board.Width - 1; x++)
            {
                result.Add(new Vector2Int(x, y));
            }
        }

        return result;
    }

    public override List<Vector2Int> GetHoverPositions(Vector2Int position)
    {
        var result = new List<Vector2Int>();
        var positions = Board.GetAllInnerPositions();
        foreach (var pos in positions)
        {
            if (Math.Abs(position.y - pos.y) <= 1)
                result.Add(pos);
        }

        return result;
    }
}

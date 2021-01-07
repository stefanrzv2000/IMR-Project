using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellWaterMissile : CardSpell
{
    private const int DamageDealt = 1;
    private const int NR_CASTS = 3;
    public CardSpellWaterMissile(int owner)
    {
        Name = "Water Missiles";
        Description = $"Deals <b>{DamageDealt} Damage</b> to a random dragon. Casts <b>{NR_CASTS}</b> times.";
        Race = WATER;
        ID = WATER_MISSILE;
        Owner = owner;
        ManaCost = 1;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        var allDragons = Board.GetAllDragons();

        for (int i = 0; i < NR_CASTS; i++)
        {
            if (allDragons.Count > 0)
            {
                int index = Random.Range(0, allDragons.Count);
                allDragons[index].ReceiveDamage(DamageDealt);
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

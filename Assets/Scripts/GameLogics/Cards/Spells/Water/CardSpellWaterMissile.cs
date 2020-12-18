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
        Description = $"Deals {DamageDealt} to a random dragon. Casts {NR_CASTS} times";
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
}

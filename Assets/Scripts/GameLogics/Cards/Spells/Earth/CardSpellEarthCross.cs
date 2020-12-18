using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellEarthCross : CardSpell
{
    private const int DamageDealt = 5;
    public CardSpellEarthCross(int owner)
    {
        Name = "Earth Stone Cross";
        Description = $"Deals {DamageDealt} enemies in the selected cross(+) area";
        ID = EARTH_CROSS;
        Owner = owner;
        ManaCost = 4;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        var allDragons = Board.GetAllDragons();
        foreach (var dragon in allDragons)
        {
            if (dragon.DistanceTo(targetPosition) < 1)
                dragon.ReceiveDamage(DamageDealt);
        }
    }
}

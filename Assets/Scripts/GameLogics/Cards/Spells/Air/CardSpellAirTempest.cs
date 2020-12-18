using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellAirTempest : CardSpell
{
    private const int BonusSpeed = 3;
    private const int BonusPermanentSpeed = 1;
    public CardSpellAirTempest(int owner)
    {
        Name = "Air Tempest";
        Description = $"Gives all your dragons {BonusSpeed} bonus move speed";
        ID = AIR_TEMPEST;
        Owner = owner;
        ManaCost = 6;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        var allyDragons = Board.GetAllDragonsOfOwner(Owner);
        foreach (var dragon in allyDragons)
        {
            dragon.SpeedRemained += BonusSpeed;
            dragon.Speed += BonusPermanentSpeed;
        }
    }
}

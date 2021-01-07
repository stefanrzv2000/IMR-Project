using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CardSpellAirTempest : CardSpell
{
    private const int BonusSpeed = 3;
    private const int BonusPermanentSpeed = 1;
    public CardSpellAirTempest(int owner)
    {
        Name = "Air Tempest";
        Description = $"Gives all your dragons {BonusSpeed} bonus move speed";
        Race = AIR;
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

    public override List<Vector2Int> GetAvailableTargets()
    {
        var allyDragons = Board.GetAllDragonsOfOwner(Owner);
        if (allyDragons.Count > 0)
        {
            return Board.GetAllInnerPositions();
        }
        return new List<Vector2Int>();
    }

    public override List<Vector2Int> GetHoverPositions(Vector2Int position)
    {
        return Board.GetAllInnerPositions();
    }
}

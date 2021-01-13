using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellEarthSlow : CardSpell
{
    private const int PermanentSlow = 2;
    public CardSpellEarthSlow(int owner)
    {
        Name = "Earth Slow";
        Description = $"Slows by {PermanentSlow} an enemy dragon. Can root enemies!";
        Race = EARTH;
        ID = EARTH_SLOW;
        Owner = owner;
        ManaCost = 3;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;
        var selectedDragon = (OnBoardDragon)Board.Destructables[y, x];

        selectedDragon.ReceiveBuff(0, 0, 0, -PermanentSlow, true, $"-{PermanentSlow}");
        // selectedDragon.Speed = Math.Max(0, selectedDragon.Speed - PermanentSlow);
        // selectedDragon.SpeedRemained = Math.Max(0, selectedDragon.SpeedRemained - PermanentSlow);
    }

    public override List<Vector2Int> GetAvailableTargets()
    {
        List<Vector2Int> result = new List<Vector2Int>();

        var allyDragons = Board.GetAllDragonsOfOwner(Owner);
        foreach (var dragon in allyDragons)
        {
            result.Add(new Vector2Int(dragon.BoardX, dragon.BoardY));
        }

        return result;
    }
}

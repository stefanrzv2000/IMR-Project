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
        ID = EARTH_SLOW;
        Owner = owner;
        ManaCost = 3;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;
        var selectedDragon = (OnBoardDragon)Board.Destructables[y, x];

        selectedDragon.Speed = Math.Max(0, selectedDragon.Speed - PermanentSlow);
        selectedDragon.SpeedRemained = Math.Max(0, selectedDragon.SpeedRemained - PermanentSlow);
    }
}

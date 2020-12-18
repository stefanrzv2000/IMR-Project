using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellWaterTideWave : CardSpell
{
    private const int Bonus = 1;

    public CardSpellWaterTideWave(int owner)
    {
        Name = "Water Tide Wave";
        Description = $"Blesses all the ally dragons from selected and adjacent column," +
                      $"and slows permanently all the enemy dragons selected and adjacent column." +
                      $"Can root enemies!";
        ID = WATER_TIDE_WAVE;
        Owner = owner;
        ManaCost = 8;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int x = targetPosition.x;
        var allDragons = Board.GetAllDragons();
        foreach (var dragon in allDragons)
        {
            if (Math.Abs(dragon.BoardY - x) <= 1)
            {
                if (dragon.Owner == Owner)
                {
                    dragon.Attack += Bonus;
                    dragon.Speed += Bonus;
                    dragon.SpeedRemained += Bonus;
                    dragon.Range += Bonus;
                    dragon.MaxHealth += Bonus;
                    dragon.Health += Bonus;
                }
                else
                {
                    dragon.Speed = Math.Max(0, dragon.Speed - 1);
                    dragon.SpeedRemained = Math.Max(0, dragon.SpeedRemained - 1);
                }
            }
        }
    }
}

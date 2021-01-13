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
        Race = WATER;
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
            Debug.Log("I have a dragon");
            if (Math.Abs(dragon.BoardX - x) <= 1)
            {
                if (dragon.Owner == Owner)
                {
                    dragon.ReceiveBuff(Bonus, Bonus, Bonus, Bonus, star: true, textUpdate: $"+{Bonus}");
                    //dragon.Attack += Bonus;
                    //dragon.Speed += Bonus;
                    //dragon.SpeedRemained += Bonus;
                    //dragon.Range += Bonus;
                    //dragon.MaxHealth += Bonus;
                    //dragon.Health += Bonus;
                }
                else
                {
                    dragon.ReceiveBuff(0, 0, 0, -1, true, "-1");
                    //dragon.Speed = Math.Max(0, dragon.Speed - 1);
                    //dragon.SpeedRemained = Math.Max(0, dragon.SpeedRemained - 1);
                }
            }
        }
    }

    public override List<Vector2Int> GetAvailableTargets()
    {
        List<Vector2Int> result = new List<Vector2Int>();
        for (int y = 1; y < Board.Height - 1; y++)
        {
            for (int x = 2; x < Board.Width - 2; x++)
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
            if (Math.Abs(position.x - pos.x) <= 1)
                result.Add(pos);
        }

        return result;
    }
}

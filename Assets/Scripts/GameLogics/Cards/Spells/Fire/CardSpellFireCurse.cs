using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardSpellFireCurse : CardSpell
{
    private const int DamageReduce = 2;
    private const int NR_ENEMIES_CURSED = 2;
    private const int NR_RANDOM_CURSED = 2;
    public CardSpellFireCurse(int owner)
    {
        Name = "Fire Curse";
        Description = $"Makes {NR_ENEMIES_CURSED + NR_RANDOM_CURSED} random selections: " +
                      $"{NR_ENEMIES_CURSED} on enemy," + $" {NR_RANDOM_CURSED} on anyone, " +
                      $"reducing the Attack of the selected dragons by {DamageReduce}";
        Race = FIRE;
        ID = FIRE_CURSE;
        Owner = owner;
        ManaCost = 5;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        var allDragons = Board.GetAllDragons();
        var enemyDragons = Board.GetAllDragonsOfOwner(3 - Owner);

        var selectedDragons = new List<OnBoardDragon>();

        for (int i = 0; i < NR_ENEMIES_CURSED; i++)
        {
            if (enemyDragons.Count > 0)
            {
                int index = Random.Range(0, enemyDragons.Count);
                selectedDragons.Add(enemyDragons[index]);
            }
        }

        for (int i = 0; i < NR_RANDOM_CURSED; i++)
        {
            if (allDragons.Count > 0)
            {
                int index = Random.Range(0, allDragons.Count);
                selectedDragons.Add(allDragons[index]);
            }
        }

        foreach (var dragon in selectedDragons)
        {
            dragon.Attack = Math.Max(0, dragon.Attack - DamageReduce);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellWaterMermaidKiss : CardSpell
{
    private const int Bonus = 1;
    private const int NR_CASTS = 2;
    public CardSpellWaterMermaidKiss(int owner)
    {
        Name = "Water Mermaid Kiss";
        Description = $"Get ownership over {NR_CASTS} random enemy dragons";
        Race = WATER;
        ID = WATER_MERMAID_KISS;
        Owner = owner;
        ManaCost = 6;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        for (int i = 0; i < NR_CASTS; i++)
        {
            var enemyDragons = Board.GetAllDragonsOfOwner(3 - Owner);
            if (enemyDragons.Count > 0)
            {
                int index = Random.Range(0, enemyDragons.Count);
                enemyDragons[index].ChangeOwner();
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

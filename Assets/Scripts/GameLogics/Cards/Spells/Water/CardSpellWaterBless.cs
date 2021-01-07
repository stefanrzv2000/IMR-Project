using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellWaterBless : CardSpell
{
    private const int Bonus = 1;
    private const int NR_CASTS = 3;
    public CardSpellWaterBless(int owner)
    {
        Name = "Water Bless";
        Description = $"Gives to random ally dragon +{Bonus} to all stats. Casts {NR_CASTS} times";
        Race = WATER;
        ID = WATER_BLESS;
        Owner = owner;
        ManaCost = 5;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        var allyDragons = Board.GetAllDragonsOfOwner(Owner);

        for (int i = 0; i < NR_CASTS; i++)
        {
            if (allyDragons.Count > 0)
            {
                int index = Random.Range(0, allyDragons.Count);
                allyDragons[index].Attack += Bonus;
                allyDragons[index].Speed += Bonus;
                allyDragons[index].SpeedRemained += Bonus;
                allyDragons[index].Range += Bonus;
                allyDragons[index].MaxHealth += Bonus;
                allyDragons[index].Health += Bonus;
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

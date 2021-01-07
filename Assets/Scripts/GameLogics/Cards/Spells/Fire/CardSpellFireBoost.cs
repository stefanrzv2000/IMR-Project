using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellFireBoost : CardSpell
{
    private const int BonusAttack = 2;

    public CardSpellFireBoost(int owner)
    {
        Name = "Fire Boost";
        Description = $"Gives +{BonusAttack} Attack to an ally dragon";
        Race = FIRE;
        ID = FIRE_BOOST;
        Owner = owner;
        ManaCost = 2;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;
        var selectedDragon = (OnBoardDragon)Board.Destructables[y, x];
        selectedDragon.Attack += BonusAttack;
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

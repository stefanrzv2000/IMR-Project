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
}

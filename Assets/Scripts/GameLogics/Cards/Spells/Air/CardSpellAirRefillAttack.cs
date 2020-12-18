using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellAirRefillAttack : CardSpell
{
    public CardSpellAirRefillAttack(int owner)
    {
        Name = "Air Refill Attack";
        Description = $"Restores the ability to attack to a dragon";
        ID = AIR_REFILL_ATTACK;
        Owner = owner;
        ManaCost = 2;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;
        var selectedDragon = (OnBoardDragon)Board.Destructables[y, x];
        selectedDragon.CanAttack = true;
    }
}

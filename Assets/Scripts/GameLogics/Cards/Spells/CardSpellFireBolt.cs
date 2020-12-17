using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellFireBolt : CardSpell
{
    public CardSpellFireBolt(int owner)
    {
        Name = "Fire Bolt";
        ID = FIREBOLT_ID;
        Owner = owner;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;
        OnBoardDestructible attackedDragon = Board.Destructables[y, x];
        attackedDragon.ReceiveDamage(4);
    }
}

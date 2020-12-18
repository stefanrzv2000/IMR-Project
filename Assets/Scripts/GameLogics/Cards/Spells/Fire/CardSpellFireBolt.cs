using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellFireBolt : CardSpell
{
    private const int DamageDealt = 3;
    public CardSpellFireBolt(int owner)
    {
        Name = "Fire Bolt";
        Description = $"Deals {DamageDealt} damage to an enemy dragon";
        ID = FIRE_BOLT;
        Owner = owner;
        ManaCost = 2;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;
        OnBoardDestructible attackedDestructible = Board.Destructables[y, x];
        attackedDestructible.ReceiveDamage(DamageDealt);
    }
}
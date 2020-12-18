using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellWaterFrost : CardSpell
{
    private const int DamageDealt = 3;
    public CardSpellWaterFrost(int owner)
    {
        Name = "Water Frost";
        Description = $"Deals {DamageDealt} damage";
        ID = WATER_FROST;
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

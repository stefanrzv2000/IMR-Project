using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellAirLightning : CardSpell
{
    private const int DamageDealt = 4;
    public CardSpellAirLightning(int owner)
    {
        Name = "Air Lightning";
        Description = $"Deals {DamageDealt} damage to a dragon";
        ID = AIR_LIGHTNING;
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

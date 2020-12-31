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

    public override List<Vector2Int> GetAvailableTargets()
    {
        List<Vector2Int> result = new List<Vector2Int>();

        var enemyDragons = Board.GetAllDragonsOfOwner(3 - Owner);
        foreach (var dragon in enemyDragons)
        {
            result.Add(new Vector2Int(dragon.BoardX, dragon.BoardY));
        }

        return result;
    }
}

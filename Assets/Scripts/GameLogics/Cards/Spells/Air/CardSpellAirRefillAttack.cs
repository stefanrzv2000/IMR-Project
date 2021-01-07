using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellAirRefillAttack : CardSpell
{
    public CardSpellAirRefillAttack(int owner)
    {
        Name = "Air Refill Attack";
        Description = $"Restores the ability to attack to a dragon";
        Race = AIR;
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

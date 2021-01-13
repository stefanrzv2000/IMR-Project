using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellEarthFortify : CardSpell
{
    private const int PermanentHealth = 2;
    public CardSpellEarthFortify(int owner)
    {
        Name = "Earth Fortify";
        Description = $"Gives {PermanentHealth} to an ally dragon";
        Race = EARTH;
        ID = EARTH_FORTIFY;
        Owner = owner;
        ManaCost = 1;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;
        OnBoardDragon buffedDragon = (OnBoardDragon)Board.Destructables[y, x];
        buffedDragon.ReceiveBuff(PermanentHealth, 0, 0, 0, false, $"+{PermanentHealth}");
        //buffedDragon.MaxHealth += PermanentHealth;
        //buffedDragon.Health    += PermanentHealth;
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

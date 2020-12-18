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
        ID = EARTH_FORTIFY;
        Owner = owner;
        ManaCost = 1;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;
        OnBoardDestructible buffedDragon = Board.Destructables[y, x];
        buffedDragon.MaxHealth += PermanentHealth;
        buffedDragon.Health    += PermanentHealth;
    }
}

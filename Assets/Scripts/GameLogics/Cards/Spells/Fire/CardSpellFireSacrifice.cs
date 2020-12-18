using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellFireSacrifice : CardSpell
{
    private const int DamageReduce = 2;
    public CardSpellFireSacrifice(int owner)
    {
        Name = "Fire Sacrifice";
        Description = $"Sacrifices an ally dragon, giving half of their Attack and Range to all allies";
        ID = FIRE_SACRIFICE;
        Owner = owner;
        ManaCost = 8;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int yy = targetPosition.y;
        int xx = targetPosition.x;

        var selectedDragon = (OnBoardDragon)Board.Destructables[yy, xx];
        int bonusAttack = selectedDragon.Attack / 2;
        int bonusRange = selectedDragon.Range / 2;
        
        selectedDragon.ReceiveDamage(selectedDragon.Health);

        var allyDragons = Board.GetAllDragonsOfOwner(Owner);
        foreach (var dragon in allyDragons)
        {
            dragon.Attack += bonusAttack;
            dragon.Range += bonusRange;
        }
    }
}

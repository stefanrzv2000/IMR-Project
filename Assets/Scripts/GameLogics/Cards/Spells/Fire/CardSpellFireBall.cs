using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellFireBall : CardSpell
{
    private const int DamageDealt = 2;
    public CardSpellFireBall(int owner)
    {
        Name = "Fire Ball";
        Description = $"Deals {DamageDealt} damage in 3x3 Area";
        ID = FIRE_BALL;
        Owner = owner;
        ManaCost = 4;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int yy = targetPosition.y;
        int xx = targetPosition.x;

        for (int x = xx - 1; x <= xx + 1; x++)
        {
            for (int y = yy - 1; y <= yy + 1; y++)
            {
                if (x < 0 || x >= Board.Width || 
                    y < 0 || y >= Board.Height ||
                    Board.Destructables[y, x] == null ||
                    Board.Destructables[y, x].DestructibleType == OCCUPIED)
                    continue;

                Board.Destructables[y, x].ReceiveDamage(DamageDealt);
            }
        }
    }
}

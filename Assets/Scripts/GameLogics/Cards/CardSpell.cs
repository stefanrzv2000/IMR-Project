using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpell : Card
{
    public int ID;

    protected const int FIREBOLT_ID = 0;

    public CardSpell()
    {
        CardType = CardType.SPELL;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {

    }
}
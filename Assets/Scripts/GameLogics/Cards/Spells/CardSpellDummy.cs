using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellDummy : CardSpell
{
    public CardSpellDummy(int owner)
    {
        Name = "Dummy";
        ID = -1;
        Owner = owner;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {

    }
}

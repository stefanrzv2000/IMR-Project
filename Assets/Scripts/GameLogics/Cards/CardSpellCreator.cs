using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellCreator 
{
    protected const int FIREBOLT_ID = 0;
    public static CardSpell GenerateCardSpell(int id, int owner)
    {
        switch (id)
        {
            case FIREBOLT_ID:
                return new CardSpellFireBolt(owner);
            default:
                return new CardSpellDummy(owner);
        }
    }
}
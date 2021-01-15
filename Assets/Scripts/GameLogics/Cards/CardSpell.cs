using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpell : Card
{
    public int ID;
    public int Race;

    protected const int FIRE_BOLT = 0;
    protected const int FIRE_BOOST = 1;
    protected const int FIRE_BALL = 2;
    protected const int FIRE_CURSE = 3;
    protected const int FIRE_SACRIFICE = 4;

    protected const int WATER_MISSILE = 0;
    protected const int WATER_FROST = 1;
    protected const int WATER_BLESS = 2;
    protected const int WATER_MERMAID_KISS = 3;
    protected const int WATER_TIDE_WAVE = 4;

    protected const int EARTH_FORTIFY = 0;
    protected const int EARTH_SLOW = 1;
    protected const int EARTH_CROSS = 2;
    protected const int EARTH_STEEL = 3;
    protected const int EARTH_QUAKE = 4;

    protected const int AIR_LIGHTNING = 0;
    protected const int AIR_REFILL_ATTACK = 1;
    protected const int AIR_LIGHTNING_CHAIN = 2;
    protected const int AIR_TEMPEST = 3;
    protected const int AIR_TORNADO = 4;

    protected const int DESTRUCTIBLE = 0;
    protected const int DRAGON = 1;
    protected const int OCCUPIED = 2;
    protected const int BUILDING = 3;
    protected const int NEST = 4;
    protected const int BARRACK = 5;
    protected const int MAGE_TOWER = 6;

    public CardSpell()
    {
        CardType = CardType.SPELL;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {

    }

    public void PlaySpell(Vector2Int position)
    {
        int randomSeed = Time.frameCount;
        GameReferee.Instance.CallRPCMethod("PlaySpell", new int[] { position.x, position.y }, ID, Race, Owner, randomSeed);
    }
}
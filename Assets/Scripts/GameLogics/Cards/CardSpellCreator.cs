using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellCreator 
{
    protected const int FIRE_BOLT          = 0;
    protected const int FIRE_BOOST         = 1;
    protected const int FIRE_BALL          = 2;
    protected const int FIRE_CURSE         = 3;
    protected const int FIRE_SACRIFICE     = 4;

    protected const int WATER_MISSILE      = 0;
    protected const int WATER_FROST        = 1;
    protected const int WATER_BLESS        = 2;
    protected const int WATER_MERMAID_KISS = 3;
    protected const int WATER_TIDE_WAVE    = 4;

    protected const int EARTH_FORTIFY      = 0;
    protected const int EARTH_SLOW         = 1;
    protected const int EARTH_CROSS        = 2;
    protected const int EARTH_STEEL        = 3;
    protected const int EARTH_QUAKE        = 4;

    protected const int AIR_LIGHTNING      = 0;
    protected const int AIR_REFILL_ATTACK  = 1;
    protected const int AIR_LIGHTNING_CHAIN= 2;
    protected const int AIR_TEMPEST        = 3;
    protected const int AIR_TORNADO        = 4;

    public const int FIRE = 0;
    public const int WATER = 1;
    public const int EARTH = 2;
    public const int AIR = 3;

    private static CardSpell GenerateFireCardSpell(int id, int owner)
    {

        switch (id)
        {
            case FIRE_BOLT:
                return new CardSpellFireBolt(owner);

            case FIRE_BOOST:
                return new CardSpellFireBoost(owner);

            case FIRE_BALL:
                return new CardSpellFireBall(owner);

            case FIRE_CURSE:
                return new CardSpellFireCurse(owner);

            case FIRE_SACRIFICE:
                return new CardSpellFireSacrifice(owner);

            default:
                return new CardSpellDummy(owner);
        }
    }

    private static CardSpell GenerateWaterCardSpell(int id, int owner)
    {
        switch (id)
        {
            case WATER_MISSILE:
                return new CardSpellWaterMissile(owner);

            case WATER_FROST:
                return new CardSpellWaterFrost(owner);

            case WATER_BLESS:
                return new CardSpellWaterBless(owner);

            case WATER_MERMAID_KISS:
                return new CardSpellWaterMermaidKiss(owner);

            case WATER_TIDE_WAVE:
                return new CardSpellWaterTideWave(owner);

            default:
                return new CardSpellDummy(owner);
        }
    }

    private static CardSpell GenerateEarthCardSpell(int id, int owner)
    {
        switch (id)
        {
            case EARTH_FORTIFY:
                return new CardSpellEarthFortify(owner);

            case EARTH_SLOW:
                return new CardSpellEarthSlow(owner);

            case EARTH_CROSS:
                return new CardSpellEarthCross(owner);

            case EARTH_STEEL:
                return new CardSpellEarthSteel(owner);

            case EARTH_QUAKE:
                return new CardSpellEarthQuake(owner);

            default:
                return new CardSpellDummy(owner);
        }
    }

    private static CardSpell GenerateAirCardSpell(int id, int owner)
    {
        switch (id)
        {
            case AIR_LIGHTNING:
                return new CardSpellAirLightning(owner);

            case AIR_REFILL_ATTACK:
                return new CardSpellAirRefillAttack(owner);

            case AIR_LIGHTNING_CHAIN:
                return new CardSpellAirLightningChain(owner);

            case AIR_TEMPEST:
                return new CardSpellAirTempest(owner);

            case AIR_TORNADO:
                return new CardSpellAirTornado(owner);

            default:
                return new CardSpellDummy(owner);
        }
    }

    public static CardSpell GenerateCardSpell(int id, int owner, int race)
    {
        switch (race)
        {
            case FIRE: 
                return GenerateFireCardSpell(id, owner);

            case WATER: 
                return GenerateWaterCardSpell(id, owner);

            case EARTH:
                return GenerateEarthCardSpell(id, owner);

            case AIR:
                return GenerateAirCardSpell(id, owner);

            default:
                return new CardSpellDummy(owner);
        }
    }
}
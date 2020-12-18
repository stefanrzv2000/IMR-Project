using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    NONE,
    DRAGON,
    SPELL,
}

public abstract class Card  
{
    public Board Board;

    public Material ImageMaterial;
    public int GoldCost;
    public int FoodCost;
    public int ManaCost;

    public CardType CardType = CardType.NONE;

    public int Owner;
    public string Name;
    public string Description;

    public const int FIRE = 0;
    public const int WATER = 1;
    public const int EARTH = 2;
    public const int AIR = 3;

    public const int PAWN = 0;
    public const int TANK = 1;
    public const int MELEE = 2;
    public const int RANGER = 3;
    public const int QUEEN = 4;

    public GameObject PhysicInstance;
    public abstract void GoPlay(Vector2Int targetPosition);

    public bool CanBePlayed(int gold, int food, int mana)
    {
        return gold >= GoldCost && food >= FoodCost && mana >= ManaCost;
    }
}
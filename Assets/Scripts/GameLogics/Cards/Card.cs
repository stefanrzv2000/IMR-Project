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
    public int Index = -1;

    public Board Board;

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

    public bool CanBePlayed()
    {
        int gold, food, mana;
        gold = Board.GameReferee.Players[Owner-1].Gold;
        food = Board.GameReferee.Players[Owner-1].Food;
        mana = Board.GameReferee.Players[Owner-1].Mana;
        return gold >= GoldCost && food >= FoodCost && mana >= ManaCost;
    }

    public virtual List<Vector2Int> GetAvailableTargets()
    {
        return new List<Vector2Int>();
    }

    public virtual List<Vector2Int> GetHoverPositions(Vector2Int position)
    {
        var result = new List<Vector2Int>();
        result.Add(position);
        return result;
    }
}
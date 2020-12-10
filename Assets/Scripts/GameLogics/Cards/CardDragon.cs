using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragon : Card
{
    public int MaxHealth;
    public int Speed;
    public int Attack;
    public int Range;

    public int Type;
    public int Race;

    public CardDragon(int type, int race, int owner)
    {
        Type = type;
        Race = race;
        Owner = owner;

        switch (type)
        {
            case TANK:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 1;
                Name = "Tank";

                GoldCost = 0;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case MELEE:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 1;
                Name = "Melee";

                GoldCost = 0;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case RANGER:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 5;
                Name = "Ranger";

                GoldCost = 0;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case QUEEN:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 1;
                Name = "Queen";

                GoldCost = 0;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case PAWN:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 1;
                Name = "Pawn";

                GoldCost = 0;
                FoodCost = 1;
                ManaCost = 0;
                break;

            default:
                MaxHealth = 0;
                Speed = 0;
                Attack = 0;
                Range = 0;
                Name = "???";

                GoldCost = 0;
                FoodCost = 0;
                ManaCost = 0;
                break;
        }
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        OnBoardDragon onBoardDragon = new OnBoardDragon(targetPosition, Board, this); 
    }
}

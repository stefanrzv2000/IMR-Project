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
        CardType = CardType.DRAGON;

        Type = type;
        Race = race;
        Owner = owner;

        switch (type)
        {
            case TANK:
                MaxHealth = 10;
                Speed = 1;
                Attack = 2;
                Range = 1;
                Name = "Tank";

                GoldCost = 3;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case MELEE:
                MaxHealth = 6;
                Speed = 3;
                Attack = 3;
                Range = 1;
                Name = "Melee";

                GoldCost = 4;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case RANGER:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 3;
                Name = "Ranger";

                GoldCost = 4;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case QUEEN:
                MaxHealth = 3;
                Speed = 2;
                Attack = 1;
                Range = 1;
                Name = "Queen";

                GoldCost = 5;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case PAWN:
                MaxHealth = 4;
                Speed = 2;
                Attack = 1;
                Range = 1;
                Name = "Pawn";

                GoldCost = 1;
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
        int[] target = { targetPosition.y, targetPosition.x };
        
        GameReferee.Instance.CallRPCMethod("CreateOnBoardDragon", target, Type, Race, Owner);
        //OnBoardDragon onBoardDragon = new OnBoardDragon(targetPosition, Board, this); 
    }

    public override List<Vector2Int> GetAvailableTargets()
    {
        List<Vector2Int> result = new List<Vector2Int>();

        var positions = Board.GetEmptyPositions();

        foreach(var pos in positions){
            //Debug.Log($"pos {pos}");
            if(PlayerInfoScene.Instance.playerId == 1)
            {
                if (pos.y >= 5)
                {
                    result.Add(pos);
                }
            }
            else
            {
                if (pos.y <= 2)
                {
                    result.Add(pos);
                }
            }
        }

        return result;
    }
}

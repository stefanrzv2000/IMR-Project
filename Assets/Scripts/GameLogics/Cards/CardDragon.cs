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
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 1;
                Name = "Tank";

                GoldCost = 1;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case MELEE:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 1;
                Name = "Melee";

                GoldCost = 2;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case RANGER:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 5;
                Name = "Ranger";

                GoldCost = 3;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case QUEEN:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 1;
                Name = "Queen";

                GoldCost = 5;
                FoodCost = 1;
                ManaCost = 0;
                break;

            case PAWN:
                MaxHealth = 4;
                Speed = 2;
                Attack = 2;
                Range = 1;
                Name = "Pawn";

                GoldCost = 4;
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

        //PhysicInstance = CardsGenerator.CreateCard(1, 0.1f, 270, FIRE, TANK);
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        GameReferee.Instance.photonView.RPC("CreateOnBoardDragon", Photon.Pun.RpcTarget.All, targetPosition, this);
        //OnBoardDragon onBoardDragon = new OnBoardDragon(targetPosition, Board, this); 
    }

    public override List<Vector2Int> GetAvailableTagets()
    {
        List<Vector2Int> result = new List<Vector2Int>();

        var positions = Board.GetEmptyPositions();

        foreach(var pos in positions){
            //Debug.Log($"pos {pos}");
            if(PlayerInfoScene.Instance.playerId == 1)
            {
                if (pos.x >= 5)
                {
                    result.Add(pos);
                }
            }
            else
            {
                if (pos.x <= 2)
                {
                    result.Add(pos);
                }
            }
        }

        return result;
    }
}

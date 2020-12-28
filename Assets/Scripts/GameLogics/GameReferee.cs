using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReferee : MonoBehaviour
{
    public Board Board;

    private GamePlayer[] Players;

    public int GoldBonus;
    public int MaxManaBonus;
    public int MaxFoodBonus;

    private int FIRE = 0;
    private int WATER = 1;
    private int EARTH = 2;
    private int AIR = 3;

    private const int NR_CARD_DRAGONS_START = 3;
    private const int NR_CARD_SPELLS_START = 2;

    public GameObject CardPrefab;
    private CardsGenerator physicalCardGenerator;

    void Start()
    {
        Board = new Board();
        //Debug.Log("Direct mesajul");

        physicalCardGenerator = new CardsGenerator(CardPrefab);

        GoldBonus = 5;
        MaxManaBonus = 1;
        MaxFoodBonus = 1;

        Players = new GamePlayer[2];
        Players[0] = new GamePlayer(1, FIRE,  Board, true, physicalCardGenerator);
        Players[1] = new GamePlayer(2, WATER, Board, false, physicalCardGenerator);
        //Debug.Log("Direct mesajul");

        for (int i = 0; i < NR_CARD_DRAGONS_START; i++) 
        {
            GiveCardDragon(0);
            GiveCardDragon(1);
            Debug.Log($"Given {i+1} card");
        }
        //Debug.Log("Direct mesajul");

        for (int i = 0; i < NR_CARD_SPELLS_START; i++)
        {
            GiveCardSpell(0);
            GiveCardSpell(1);
        }
        //Debug.Log("Direct mesajul");
    }

    void GiveCardDragon(int index)
    {
        var barrack = (OnBoardBarrack)Board.Buildings[3 * index + 1];
        Card card = barrack.ResetTurn(Players[index].Race);
        card.Board = Board;
        Players[index].ReceiveCard(card);
    }

    void GiveCardSpell(int index)
    {
        var mageTower = (OnBoardMageTower)Board.Buildings[3 * index + 2];
        Card card = mageTower.ResetTurn(Players[index].Race);
        card.Board = Board;
        Players[index].ReceiveCard(card);
    }

    void PassTurnToPlayer(int index)
    {
        Players[1-index].EndedTurn = false;
        Players[index].HisTurn = true;
        Players[index].ResetTurn(GoldBonus, MaxManaBonus, MaxFoodBonus);

        GiveCardDragon(index);
        GiveCardSpell(index);
        Board.ResetTurn(Players[index].ID);
    }
    // Update is called once per frame
    void Update()
    {
        if (Players[0].EndedTurn)
        {
            PassTurnToPlayer(1);
        }
        else if (Players[1].EndedTurn)
        {
            PassTurnToPlayer(0);
        }
    }
}
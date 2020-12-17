using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReferee : MonoBehaviour
{
    public Board Board;

    private Player[] Players;

    public int GoldBonus;
    public int MaxManaBonus;
    public int MaxFoodBonus;

    private int FIRE = 0;
    private int WATER = 1;
    private int EARTH = 2;
    private int AIR = 3;

    private const int NR_CARD_DRAGONS_START = 3;
    private const int NR_CARD_SPELLS_START = 2;
    void Start()
    {
        Board = new Board();
        Debug.Log("Direct mesajul");

        GoldBonus = 5;
        MaxManaBonus = 1;
        MaxFoodBonus = 1;

        Players = new Player[2];
        Players[0] = new Player(1, FIRE,  Board, true);
        Players[1] = new Player(2, WATER, Board, false);
        Debug.Log("Direct mesajul");

        for (int i = 0; i < NR_CARD_DRAGONS_START; i++) 
        {
            GiveCardDragon(0);
            GiveCardDragon(1);
        }
        Debug.Log("Direct mesajul");

        for (int i = 0; i < NR_CARD_SPELLS_START; i++)
        {
            GiveCardSpell(0);
            GiveCardSpell(1);
        }
        Debug.Log("Direct mesajul");
    }

    void GiveCardDragon(int index)
    {
        var Barrack = (OnBoardBarrack)Board.Buildings[3 * index + 1];
        Card card = Barrack.ResetTurn(Players[index].Race);
        Players[index].ReceiveCard(card);
    }

    void GiveCardSpell(int index)
    {
        var MageTower = (OnBoardMageTower)Board.Buildings[3 * index + 2];
        Card card = MageTower.ResetTurn();
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
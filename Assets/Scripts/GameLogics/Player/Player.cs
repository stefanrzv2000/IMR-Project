using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public int ID;
    public int Race;
    public Board Board;

    public bool HisTurn;
    public bool EndedTurn;
    public bool TurnEnded;

    public CardHolder CardHolder;
    
    public int Gold;
    public int Food;
    public int Mana;
    public int MaxFood;
    public int MaxMana;

    public Player(int id, int race, Board board, bool hisTurn)
    {
        Race = race;
        ID = id;
        Board = board;
        HisTurn = hisTurn;
        CardHolder = new CardHolder(id);

        Gold = 0;
        Food = 2;
        Mana = 1;
        MaxFood = 2;
        MaxMana = 1;
        EndedTurn = false;
    }

    public void UseCard(int index, Vector2Int target)
    {
        CardHolder.UseCard(index, target);
    }

    public void ReceiveCard(Card card)
    {
        CardHolder.ReceiveCard(card);
    }

    public void ResetTurn(int goldBonus, int maxManaBonus, int maxFoodBonus)
    {
        Gold += goldBonus;

        MaxMana += maxManaBonus;
        Mana = MaxMana;

        MaxFood += maxFoodBonus;
        Food = MaxFood;

        HisTurn = true;
    }

    public void EndTurn()
    {
        HisTurn = false;
        EndedTurn = true;
    }
}
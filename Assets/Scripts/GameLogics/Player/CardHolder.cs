using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder 
{
    private const int MAX_CARDS = 10;

    public List<Card> Cards;
    public int Owner;

    public CardHolder(int owner)
    {
        Cards = new List<Card>();
        Owner = owner;
    }

    public bool ReceiveCard(Card card)
    {
        if (Cards.Capacity == MAX_CARDS)
            return false;

        Cards.Add(card);
        return true;
    }

    public void UseCard(int index, Vector2Int target)
    {
        Cards[index].GoPlay(target);
        Cards.RemoveAt(index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder 
{
    public CardsGenerator cardGenerator;
    private const int MAX_CARDS = 10;

    public List<Card> Cards;
    public int Owner;

    private CardsGenerator physicalCardGenerator;

    public CardHolder(int owner, CardsGenerator cardGen)
    {
        physicalCardGenerator = cardGen;

        Cards = new List<Card>();
        Owner = owner;
    }

    public bool ReceiveCard(Card card)
    {
        if (Cards.Capacity == MAX_CARDS)
            return false;

        if(card.CardType == CardType.DRAGON && Owner == 1)
        {
            GameObject physicalCard = physicalCardGenerator.CreateCard(Cards.Count,((CardDragon)card).Race, ((CardDragon)card).Type);
            card.PhysicInstance = physicalCard;
        }

        Cards.Add(card);
        return true;
    }

    public void UseCard(int index, Vector2Int target)
    {
        Cards[index].GoPlay(target);
        Cards.RemoveAt(index);
    }
}
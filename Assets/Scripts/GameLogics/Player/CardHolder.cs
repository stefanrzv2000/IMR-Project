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
        Debug.Log($"Cards Count {Cards.Count}");
        if (Cards.Count == MAX_CARDS)
            return false;

        if(card.CardType == CardType.DRAGON && Owner == PlayerInfoScene.Instance.playerId)
        {
            float angle = 450 - 180 * Owner;
            GameObject physicalCard = physicalCardGenerator.CreateCard(Cards.Count,(CardDragon)card, angle:angle);
            card.PhysicInstance = physicalCard;
            physicalCard.GetComponent<PhysicalCardInteractor>().virtualCard = card;
        }
        if (card.CardType == CardType.SPELL && Owner == PlayerInfoScene.Instance.playerId)
        {
            float angle = 450 - 180 * Owner;
            GameObject physicalCard = physicalCardGenerator.CreateCard(Cards.Count, (CardSpell)card, angle: angle);
            card.PhysicInstance = physicalCard;
            physicalCard.GetComponent<PhysicalCardInteractor>().virtualCard = card;
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
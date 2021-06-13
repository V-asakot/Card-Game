using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField]
    private Card _cardPrefab;
    private Transform _container;
    public Transform Container { get { return _container; } }
    public Card CardPrefab { get { return _cardPrefab; } }
    private List<Card> _cards = new List<Card>();
    public List<Card> Cards { get { return _cards; } }
    public event Action<List<Card>> OnHandChanged = hand => { };

    void Awake()
    {
        _container = transform;
    }

    public void AddCard(CardData cardData)
    {
        AddCardWithoutUpdate(cardData);
        NotifyHandChange();
    }

    public void AddCardWithoutUpdate(CardData cardData)
    {
        var card = Instantiate(_cardPrefab);
        card.SetData(cardData);
        card.transform.SetParent(_container);
        card.OnCardDestroyed += RemoveCard;
        _cards.Add(card);
    }

    public void AddCardRange(CardData[] cards)
    {
        foreach (var cardData in cards)
        {
            AddCardWithoutUpdate(cardData);
        }
        NotifyHandChange();
    }

    public void RemoveCard(Card card)
    {
        _cards.Remove(card);
        Destroy(card.gameObject);
        NotifyHandChange();
    }

    private void NotifyHandChange()
    {
        OnHandChanged.Invoke(_cards);
    }

}

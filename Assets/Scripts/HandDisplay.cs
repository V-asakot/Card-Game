using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandDisplay : MonoBehaviour
{
    private CardsManager _cardsManager;
    private Rect _cardRect;
    [SerializeField]
    private float _fullAngle;
    [SerializeField]
    private float _spacing;
    [SerializeField]
    private float _additionalY;

    public float CardWidth() => _cardRect.width;

    public void Awake()
    {
        _cardsManager = GetComponent<CardsManager>();
        _cardRect = _cardsManager.CardPrefab.transform.Find("Background").GetComponent<RectTransform>().rect;
        _cardsManager.OnHandChanged += DisplayCards;
    }

    public void DisplayCards(List<Card> cards)
    {
        var fullAngle = -_fullAngle;
        var anglePerCard = fullAngle / cards.Count;
        var firstAngle = CalculateFirstCardAngle(fullAngle);
        var width = CalculateWidth(cards.Count);
        var offsetX = - width / 2;

        for (var i = 0; i < cards.Count; i++)
        {
            var card = cards[i];

            //Угол наклона карты
            var angleZ = firstAngle + i * anglePerCard;

            card.transform.rotation = Quaternion.Euler(0, 0, angleZ);

            //Позиция карты
            var xPos = (offsetX + CardWidth() / 2);
            var yDistance = Mathf.Abs(angleZ) * _additionalY;
            var yPos = - yDistance;
            card.transform.localPosition = new Vector3(xPos, yPos, card.transform.position.z);

            //увеличиваем смещение
            offsetX += CardWidth() + _spacing;
        }

    }
    private static float CalculateFirstCardAngle(float fullAngle)
    {
        var magicMathFactor = 0.1f;
        return -(fullAngle / 2) + fullAngle * magicMathFactor;
    }

    private float CalculateWidth(int quantityOfCards)
    {
        var widthCards = quantityOfCards * CardWidth();
        var widthSpacing = (quantityOfCards - 1) * _spacing;
        return widthCards + widthSpacing;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCards : MonoBehaviour
{
    [SerializeField]
    CardsManager _cardsManager;

    public void RandomlyChange() {
        StartCoroutine(RandomlyChangeIEnumerator());
    }

    IEnumerator RandomlyChangeIEnumerator()
    {
        var cards = _cardsManager.Cards;
        for (int i = 0; i < cards.Count; i++)
        {
            switch (UnityEngine.Random.Range(0, 3)) 
            {
                case 0: cards[i].AddMana(UnityEngine.Random.Range(-5, 5)); yield return new WaitForSeconds(2f); break;
                case 1: cards[i].AddDamage(UnityEngine.Random.Range(-5, 5)); yield return new WaitForSeconds(2f); break;
                case 2: cards[i].AddHealth(UnityEngine.Random.Range(-5, 5)); yield return new WaitForSeconds(2f); break;
            }
        }
        
    }
}

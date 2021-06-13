using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardsDataLoader : MonoBehaviour
{

    private CardsManager _cardsManager;
    private CardData[] _data;
    [SerializeField]
    GameObject _cover;
    public event Action<CardData[]> OnCardsDataLoaded = hand => { };
    public void Awake()
    {
        _cardsManager = GetComponent<CardsManager>();
        OnCardsDataLoaded += _cardsManager.AddCardRange;
    }

    void Start()
    {

        StartCoroutine(LoadCardsData());
       
    }

    IEnumerator LoadCardsData()
    {
        string link = "https://picsum.photos/175/145";
        int size = UnityEngine.Random.Range(4, 7);
        _data = new CardData[size];
        for (int i =0;i<size;i++)
        {
            _data[i] = ScriptableObject.CreateInstance<CardData>();
            _data[i].health = UnityEngine.Random.Range(1, 10);
            _data[i].mana = UnityEngine.Random.Range(1, 10);
            _data[i].damage = UnityEngine.Random.Range(1, 10);
            _data[i].description = "Example";
            _data[i].title = "Example";
            var www = new WWW(link);
            yield return www;
            var texture = www.texture;
            _data[i].icon = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        DisableLoadingCover();
        OnCardsDataLoaded.Invoke(_data);
    }

    public void DisableLoadingCover()
    {
        _cover.SetActive(false);
    }

    
}

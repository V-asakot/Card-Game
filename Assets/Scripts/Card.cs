using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card : MonoBehaviour
{
    private CardData _data;
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private Text _titleText;
    [SerializeField]
    private Text _descriptionText;
    [SerializeField]
    private Text _manaText;
    [SerializeField]
    private Text _damageText;
    [SerializeField]
    private Text _healthText;
    public event Action<Card> OnCardDestroyed = hand => { };
    public void SetData(CardData data) { _data = data; DisplayData(); }
    public void DisplayData()
    {
        _iconImage.sprite = _data.icon;
        _titleText.text = _data.title;
        _descriptionText.text = _data.description;
        _manaText.text = _data.mana.ToString();
        _damageText.text = _data.damage.ToString();
        _healthText.text = _data.health.ToString();
    }

    public void AddMana(int mana)
    {
        int oldValue = _data.mana;
        _data.mana = _data.mana + mana >= 0 ? _data.mana + mana : 0;
        ChangeManaAnimation(oldValue, _data.mana);
    }

    public void AddHealth(int health)
    {
        int oldValue = _data.health;
        _data.health = _data.health + health >= 0 ? _data.health + health : 0;
        ChangeHealthAnimation(oldValue, _data.health);
    }
    public void AddDamage(int damage)
    {
        int oldValue = _data.damage;
        _data.damage = _data.damage + damage >= 0 ? _data.damage + damage : 0;
        ChangeDamageAnimation(oldValue, _data.damage);
    }

    private void ChangeManaAnimation(int oldValue,int value)
    {
        var sequance = DOTween.Sequence();
        var startY = transform.localPosition.y;
        var startTextSize = _manaText.fontSize;
        _manaText.fontSize *= 2;
        sequance.Append(transform.DOLocalMoveY(300, 0.5f));
        sequance.Append(_manaText.DOCounter(oldValue, value, 0.5f));
        sequance.Append(transform.DOLocalMoveY(startY, 0.5f)).OnComplete(() => { _manaText.fontSize = startTextSize; });

    }

    private void ChangeHealthAnimation(int oldValue, int value)
    {
        var sequance = DOTween.Sequence();
        var startY = transform.localPosition.y;
        var startTextSize = _healthText.fontSize;
        _healthText.fontSize *= 2;
        sequance.Append(transform.DOLocalMoveY(300, 0.5f));
        sequance.Append(_healthText.DOCounter(oldValue,value, 0.5f));
        sequance.Append(transform.DOLocalMoveY(startY, 0.5f)).OnComplete(() => { _healthText.fontSize = startTextSize; if (value == 0) DestroyCard(); });

    }

    private void ChangeDamageAnimation(int oldValue, int value)
    {
        var sequance = DOTween.Sequence();
        var startY = transform.localPosition.y;
        var startTextSize = _damageText.fontSize;
        _damageText.fontSize *= 2;
        sequance.Append(transform.DOLocalMoveY(300, 0.5f));
        sequance.Append(_damageText.DOCounter(oldValue, value, 0.5f));
        sequance.Append(transform.DOLocalMoveY(startY, 0.5f)).OnComplete(() => { _damageText.fontSize = startTextSize; });
    }


    private void DestroyCard()
    {
        OnCardDestroyed.Invoke(this);
    }
}

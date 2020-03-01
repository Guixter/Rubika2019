using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Card card;
    public bool hidden;
    public Sprite hiddenSprite;
    public SpriteRenderer cardRenderer;

    public void SetCard(Card card, bool hidden)
    {
        this.card = card;
        this.hidden = hidden;
        
        if (hidden)
        {
            cardRenderer.sprite = hiddenSprite;
        }
        else
        {
            var spritePath = $"PlayingCards/{card.Name()}";
            var sprite = Resources.Load<Sprite>(spritePath);
            cardRenderer.sprite = sprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCard(this.card, this.hidden);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct Card
{
    public CardValue value;
    public CardType type;

    public string Name()
    {
        return $"{TypeName(type)}{ValueName(value)}";
    }

    private string TypeName(CardType type)
    {
        switch (type)
        {
            case CardType.Club:
                return "Club";
            case CardType.Diamond:
                return "Diamond";
            case CardType.Heart:
                return "Heart";
            case CardType.Spade:
                return "Spade";
        }

        return "";
    }

    private string ValueName(CardValue value)
    {
        switch (value)
        {
            case CardValue.Ace:
                return "01";
            case CardValue._2:
                return "02";
            case CardValue._3:
                return "03";
            case CardValue._4:
                return "04";
            case CardValue._5:
                return "05";
            case CardValue._6:
                return "06";
            case CardValue._7:
                return "07";
            case CardValue._8:
                return "08";
            case CardValue._9:
                return "09";
            case CardValue._10:
                return "10";
            case CardValue.Jack:
                return "11";
            case CardValue.Queen:
                return "12";
            case CardValue.King:
                return "13";
        }

        return "";
    }
}

public enum CardValue
{
    _2,
    _3,
    _4,
    _5,
    _6,
    _7,
    _8,
    _9,
    _10,
    Jack,
    Queen,
    King,
    Ace
}

public enum CardType
{
    Diamond,
    Spade,
    Club,
    Heart
}
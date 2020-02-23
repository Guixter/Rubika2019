using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static readonly int NB_CARD_PER_HAND = 2;
    public static readonly int NB_CARD_BANK = 5;


    public Player[] players;
    public GameObject cardPrefab;
    public Hand bankHand;
    public float AnimationTime;

    private Card[] deck;

    void Start()
    {
        StartCoroutine(HandleGame());
    }

    IEnumerator HandleGame()
    {
        yield return StartCoroutine(SetupGame());

        yield return new WaitForSeconds(3);

        yield return StartCoroutine(Animate(false));
    }

    IEnumerator SetupGame()
    {
        // Create a deck
        deck = new Card[52];
        int i = 0;
        foreach (CardType t in Enum.GetValues(typeof(CardType)))
        {
            foreach (CardValue v in Enum.GetValues(typeof(CardValue)))
            {
                deck[i++] = new Card
                {
                    value = v,
                    type = t
                };
            }
        }

        // Shuffle the deck
        var random = new System.Random();
        var popDeck = new Stack<Card>(deck.OrderBy(x => random.Next()).ToList());

        // Give cards to players
        foreach (var player in players)
        {
            player.hand.Draw(popDeck, NB_CARD_PER_HAND, true);
        }

        // Give cards to the bank
        bankHand.Draw(popDeck, NB_CARD_BANK, false);

        // Animate the cards in
        yield return StartCoroutine(Animate(true));
    }

    IEnumerator Animate(bool animateIn)
    {
        // Animate the cards inforeach (var player in players)
        foreach (var player in players)
        {
            yield return StartCoroutine(player.hand.Animate(AnimationTime, animateIn));
        }
        yield return StartCoroutine(bankHand.Animate(AnimationTime, animateIn));
    }
}

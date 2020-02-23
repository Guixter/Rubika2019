using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static readonly int NB_CARD_PER_HAND = 5;
        
    public Player[] players;
    public GameObject cardPrefab;
    public Hand bankHand;

    private Card[] deck;

    void Start()
    {
        StartCoroutine("HandleGame");
    }

    IEnumerator HandleGame()
    {
        // Create a deck
        deck = new Card[52];
        int i = 0;
        foreach (CardType t in Enum.GetValues(typeof(CardType)))
        {
            foreach (CardValue v in Enum.GetValues(typeof(CardValue)))
            {
                deck[i++] = new Card {
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
            player.hand.Draw(popDeck, NB_CARD_PER_HAND);
        }

        // Give cards to the bank
        bankHand.Draw(popDeck, NB_CARD_PER_HAND);

        // TODO

        return null;
    }
}

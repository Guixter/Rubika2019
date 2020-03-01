using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static readonly int NB_CARD_PER_HAND = 5;

    public Player[] players;
    public GameObject cardPrefab;
    public float AnimationTime;
    public float AnimationCardTime;
    public int nbChipsPerPlayer = 100;

    private Card[] deck;
    private List<Player> currentPlayers;

    void Start()
    {
        PreparePlayers();

        StartCoroutine(GameLoop());
    }

    void PreparePlayers()
    {
        currentPlayers = new List<Player>(players);
        foreach (var player in currentPlayers)
        {
            player.chips = nbChipsPerPlayer;
        }
    }

    IEnumerator GameLoop()
    {
        while (currentPlayers.Count > 1)
        {
            yield return SetupRound();

            yield return new WaitForSeconds(AnimationTime);

            var currentBid = -1;
            var lastBidderIndex = -1;
            var currentPlayerIndex = 0;

            while (lastBidderIndex != currentPlayerIndex)
            {
                var player = currentPlayers[currentPlayerIndex];
                var bid = player.Bid(Math.Max(currentBid, 0));

                if (bid != currentBid)
                {
                    currentBid = bid;
                    lastBidderIndex = currentPlayerIndex;
                }

                currentPlayerIndex++;
                currentPlayerIndex %= currentPlayers.Count;
            }

            // TODO winning animation
            Debug.Log($"{currentPlayerIndex} won the round");

            yield return AnimateCards(false);

            foreach (var player in currentPlayers)
            {
                player.CleanHand();
            }

            yield return new WaitForSeconds(AnimationTime);
        }
    }

    IEnumerator SetupRound()
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
        foreach (var player in currentPlayers)
        {
            player.DrawHand(popDeck, NB_CARD_PER_HAND, cardPrefab, true);
        }

        // Animate the cards in
        yield return AnimateCards(true);
    }

    IEnumerator AnimateCards(bool animateIn)
    {
        foreach (var player in currentPlayers)
        {
            yield return player.AnimateCards(AnimationCardTime, animateIn);
        }
    }
}

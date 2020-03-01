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
    public int nbChipsPerPlayer = 100;

    private Card[] deck;
    private List<Player> currentPlayers;

    void Start()
    {
        StartCoroutine(HandleGame());
    }

    IEnumerator HandleGame()
    {
        // TODO give chips to players

        currentPlayers = new List<Player>(players);
        while (currentPlayers.Count > 1)
        {
            yield return SetupGame();

            yield return new WaitForSeconds(3);

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

            yield return Animate(false);

            yield return new WaitForSeconds(3);
        }
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
        foreach (var player in currentPlayers)
        {
            player.Draw(popDeck, NB_CARD_PER_HAND, cardPrefab, true);
        }

        // Animate the cards in
        yield return Animate(true);
    }

    IEnumerator Animate(bool animateIn)
    {
        // Animate the cards inforeach (var player in players)
        foreach (var player in currentPlayers)
        {
            yield return player.Animate(AnimationTime, animateIn);
        }
    }
}

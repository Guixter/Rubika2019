using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static readonly int NB_CARD_PER_HAND = 2;
    public static readonly int NB_CARD_BANK = 5;


    public Player[] players;
    public GameObject cardPrefab;
    public Hand bankHand;
    public int AnimationTime;
    public int nbChipsPerPlayer = 100;

    private Card[] deck;
    private List<Player> currentPlayers;
    private Task manager;

    void Start()
    {
        HandleGame();
    }

    async void HandleGame()
    {
        currentPlayers = new List<Player>(players);
        while (currentPlayers.Count > 1)
        {
            await SetupGame();

            await Task.Delay(3000);

            var currentBid = -1;
            var lastBidderIndex = -1;
            var currentPlayerIndex = 0;

            while (lastBidderIndex != currentPlayerIndex)
            {
                var player = currentPlayers[currentPlayerIndex];
                var bid = player.Bid();

                if (bid != currentBid)
                {
                    currentBid = bid;
                    lastBidderIndex = currentPlayerIndex;
                }

                currentPlayerIndex++;
                currentPlayerIndex %= currentPlayers.Count;
            }

            // TODO winning animation

            await Animate(false);

            await Task.Delay(3000);
        }
    }

    async Task SetupGame()
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
            player.hand.Draw(popDeck, NB_CARD_PER_HAND, true);
            player.chips = nbChipsPerPlayer;
        }

        // Give cards to the bank
        bankHand.Draw(popDeck, NB_CARD_BANK, false);

        // Animate the cards in
        await Animate(true);
    }

    async Task Animate(bool animateIn)
    {
        // Animate the cards inforeach (var player in players)
        foreach (var player in currentPlayers)
        {
            await player.hand.Animate(AnimationTime, animateIn);
        }
        await bankHand.Animate(AnimationTime, animateIn);
    }
}

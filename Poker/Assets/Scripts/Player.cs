using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public new string name;
    public TextMesh textmesh;
    public float gapBetweenCards = 1.2f;

    internal int chips;
    internal CardManager[] hand;

    public void Start()
    {
        textmesh.text = name;
    }

    public void DrawHand(Stack<Card> deck, int nbCards, GameObject cardPrefab, bool hidden)
    {
        hand = new CardManager[nbCards];
        for (var i = 0; i < nbCards; i++)
        {
            var card = deck.Pop();
            var cardManager = Instantiate(cardPrefab);
            cardManager.transform.SetParent(transform);
            cardManager.transform.localPosition = new Vector3(GetCardPosition(nbCards, i), 0, 0);
            cardManager.transform.localRotation = Quaternion.identity;
            cardManager.SetActive(false);

            var manager = cardManager.GetComponent<CardManager>();
            manager.SetCard(card, hidden);
            hand[i] = manager;
        }
    }

    public void CleanHand()
    {
        foreach (var manager in hand)
        {
            Destroy(manager.gameObject);
        }
        hand = new CardManager[0];
    }

    float GetCardPosition(int nbCards, int index)
    {
        return nbCards / 2 * gapBetweenCards;
    }

    public IEnumerator AnimateCards(float time, bool animateIn = true)
    {
        foreach (var cardManager in hand)
        {
            cardManager.gameObject.SetActive(animateIn);
            yield return new WaitForSeconds(time);
        }
    }

    public int Bid(int currentBid)
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                // Pass
                return 0;
            case 1:
                // Follow
                return currentBid;
            default:
                // Raise
                return currentBid + Random.Range(1, 5);
        }
    }
}

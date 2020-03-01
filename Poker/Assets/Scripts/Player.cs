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

    public void Draw(Stack<Card> deck, int nbCards, GameObject cardPrefab, bool hidden)
    {
        // TODO clean the old hand

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

    private float GetCardPosition(int nbCards, int index)
    {
        index -= nbCards / 2;
        return index * gapBetweenCards;
    }

    public IEnumerator Animate(float time, bool animateIn = true)
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

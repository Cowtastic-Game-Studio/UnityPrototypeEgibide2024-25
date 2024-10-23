using System.Collections.Generic;
using UnityEngine;

public class DeckAndHandManager : MonoBehaviour
{
    public GameObject cardPrefab;  // The card prefab to instantiate
    public List<GameObject> deck = new List<GameObject>();  // List of cards in the deck
    public List<GameObject> hand = new List<GameObject>();  // Cards in the hand
    public Transform deckArea;     // Parent object for the deck
    public Transform handArea;     // Parent object for the hand
    public float cardSpacing = 1; // Space between cards

    void Start()
    {
        InitializeDeck(10);  // Create a deck with 10 cards
    }

    // Initialize a deck with a specified number of cards
    void InitializeDeck(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            // Instantiate a new card GameObject
            GameObject newCard = Instantiate(cardPrefab, deckArea);

            // Set a unique name or attributes to distinguish the cards
            newCard.name = "Card " + (i + 1);
            newCard.transform.localPosition = Vector3.zero;  // Reset position within deckArea

            // Add the card to the deck list
            deck.Add(newCard);
        }
    }

    // Call this method to remove the last 4 cards from the deck and add them to the hand
    public void MoveLastCardsToHand()
    {
        int cardsToMove = Mathf.Min(4, deck.Count);  // Move up to 4 cards, or fewer if the deck has less than 4

        for (int i = 0; i < cardsToMove; i++)
        {
            // Get the last card in the deck
            GameObject originalCard = deck[deck.Count - 1];

            // Remove the card from the deck
            deck.RemoveAt(deck.Count - 1);

            // Instantiate a new card from the prefab
            GameObject newCard = Instantiate(cardPrefab, handArea);

            // Set the properties of the new card based on the original
            newCard.name = originalCard.name; // Copy the name or set any other attributes as needed

            // If you have any specific properties to copy, you can do it here
            // For example, if your card has a script with attributes, you can copy them like this:
            // CardAttributes originalAttributes = originalCard.GetComponent<CardAttributes>();
            // CardAttributes newAttributes = newCard.GetComponent<CardAttributes>();
            // newAttributes.CopyFrom(originalAttributes); // Implement CopyFrom method to copy data

            // Add the new card to the hand
            hand.Insert(0, newCard);  // Insert at the beginning to keep the most recent on the left
        }

        // Rearrange cards in the hand after moving all cards
        ArrangeHand();
    }

    // Arranges the cards in the hand in a horizontal layout with spacing
    private void ArrangeHand()
    {
        Camera mainCamera = Camera.main;  // Get the main camera reference

        for (int i = 0; i < hand.Count; i++)
        {
            GameObject card = hand[i];
            RectTransform cardRect = card.GetComponent<RectTransform>();

            if (cardRect != null)
            {
                // For UI cards with RectTransform (2D)
                cardRect.anchoredPosition = new Vector2(i * cardSpacing, 0);
            }
            else
            {
                // For 3D cards with Transform
                // Position the card based on its index and spacing
                card.transform.localPosition = new Vector3(i * cardSpacing, 0, 0);

                // Ensure the card keeps its original rotation from the prefab
                // You can reset the rotation if necessary or keep it as it is
                card.transform.rotation = Quaternion.identity;  // Set to original prefab rotation if needed
            }
        }
    }
}

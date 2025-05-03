using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Author: Declin Anderson
// May 2, 2025

/// <summary>
/// Handles the player's hand as well as the player deck and discard
/// </summary>
public class Deck : MonoBehaviour
{
    // The Pool of cards the player has access to
    [SerializeField] public List<Card> cardPool = new List<Card>();
    // The Pool of cards in the player's hand
    [SerializeField]public List<Card> playerHand = new List<Card>();
    // The Pool of cards in the player's deck
    [SerializeField]public List<Card> deck = new List<Card>();
    // The cards in the player's discard
    [SerializeField]public List<Card> discard = new List<Card>();
    // Number of cards the player can have in their hand
    [SerializeField] public int handSize = 4;
    // the starting size or can be the max size of the player deck
    [SerializeField] public int deckSize = 10;

    // Generates the Deck when the scene is started
    public void Start()
    {
        GenerateDeck();
    }

    /// <summary>
    /// Generates the Player hand by emptying the player's current hand to the discard, then pulling from the deck and shuffling the discard in if there isn't enough to draw
    /// </summary>
    public void GenerateHand()
    {
        // If the player has card in hands when a new hand is generated then it empties it
        while(playerHand.Count > 0)
        {
            discard.Add(playerHand[playerHand.Count - 1]);
            playerHand.RemoveAt(playerHand.Count - 1);
        }
        // Adds cards to the player's hand based on their hand size
        for(int i = 0; i < handSize; i++)
        {
            // If the deck is empty
            if(deck.Count == 0)
            {
                // Shuffle the discard back into the draw pile
                while(discard.Count > 0)
                {
                    deck.Add(discard[discard.Count - 1]);
                    discard.RemoveAt(discard.Count - 1);
                }
            }
            // Pulls a random card from the draw pile and adds it to hand
            int cardPull = Random.Range(0, deck.Count);
            playerHand.Add(deck[cardPull]);
            deck.RemoveAt(cardPull);
        }
    }
    public void AddToDeck()
    {

    }

    /// <summary>
    /// Generates the Player's deck by pulling random cards from the Card Pool (Currently allows duplicates)
    /// </summary>
    public void GenerateDeck()
    {
        // Adds random cards from the card pool to the player's deck
        for(int i = 0; i < deckSize; i++)
        {
            deck.Add(cardPool[Random.Range(0, cardPool.Count)]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Author: Declin Anderson
// May 2, 2025

/// <summary>
/// Handles the player's hand as well as the player deck and discard
/// </summary>
public class Deck : MonoBehaviour
{
    // The Pool of cards the player has access to
    [SerializeField] private List<Card> cardPool = new List<Card>();
    // The Pool of cards in the player's hand
    [SerializeField] private List<Card> playerHand = new List<Card>();
    // The Pool of cards in the player's deck
    [SerializeField] private List<Card> deck = new List<Card>();
    // The cards in the player's discard
    [SerializeField] private List<Card> discard = new List<Card>();
    // The reference to all of the prefabedCards made
    [SerializeField] private List<GameObject> prefabedCards = new List<GameObject>();
    // Number of cards the player can have in their hand
    [SerializeField] private int handSize = 4;
    // For if the player has drawn cards over the handSize
    [SerializeField] private int currentHandSizeThisRound = 4;
    // The max that the player's hand size go to through upgrades
    [SerializeField] private int maxHandSize = 10;
    // the starting size or can be the max size of the player deck
    [SerializeField] private int deckSize = 10;
    // The prefab that the card is using for information
    [SerializeField] private GameObject cardPrefab;
    // The spacing between cards in your hand (visual)
    [SerializeField] private float cardSpacing = 50f;
    [SerializeField] float heightSpacing = 250f;
    [SerializeField] float currentHeightSpacing = 0f;
    [SerializeField] int newRow = 0;
    // Where in the 2d space the hand is located for cards
    [SerializeField] private Transform handPosition;

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
        currentHandSizeThisRound = handSize;
        // Destroying all of the old cards
        while(prefabedCards.Count > 0)
        {
            Destroy(prefabedCards[0]);
            prefabedCards.RemoveAt(0);
        }

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

        // Returns the x position for where the first card should be placed
        //float xPosition = cardSpacing / 2f; //-(handSize - 1)

        for (int i = 0; i < handSize; i++)
        {
            // Getting the position that the card will be shifted
            print((i + 1 )% 2);
            if (i%2 == 0 && i != 0)
            {
                currentHeightSpacing = 1 * heightSpacing;
                newRow = i;
            }
            Vector3 position = new Vector3((i - newRow )* cardSpacing, currentHeightSpacing, 0f); //xPosition + 
            // the card object being instantiated
            GameObject card = Instantiate(cardPrefab, handPosition);
            // Name for Debugging purposes
            card.gameObject.name = playerHand[i].cardName + " " + i;
            // the position of the card being adjusted
            card.transform.localPosition = position;
            // Changing the text of the card prefab to reflect what it does
            card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerHand[i].cardName;
            card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerHand[i].description;
            // Setting up the button for the cards so that when they are select they perform their action
            Button button = card.GetComponent<Button>();
            Card currentCard = playerHand[i];
            // What the button does 1. Executes Card 2. Adds card to discard 3. Removes the card from player's hand 4. destroys the cards
            /*button.onClick.AddListener(() => 
            {
                discard.Add(currentCard);
                playerHand.Remove(currentCard);               
                currentHandSizeThisRound--;
                ReloadCards();
                CardEffectParser.Instance.ExecuteCard(currentCard);   
                Destroy(card);
            });*/
            CardEffectParser.Instance.ExecuteCard(currentCard);   
            prefabedCards.Add(card);
        }
        currentHeightSpacing = 0; // reset my jank
        newRow = 0;
    }

    /// <summary>
    /// Draws a card to the player's hand
    /// </summary>
    /// <param name="amountOfCards"> The number of cards being drawn </param>
    public void DrawCard(int amountOfCards, bool handSizeWasIncreased)
    {
        // Adds cards to the player's hand based on their hand size
        for(int i = 0; i < amountOfCards; i++)
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
                if(deck.Count == 0)
                {
                    break;
                }
            }
            // Pulls a random card from the draw pile and adds it to hand
            int cardPull = Random.Range(0, deck.Count);
            playerHand.Add(deck[cardPull]);
            deck.RemoveAt(cardPull);
        }

        // If the hand size was increased the reload cards already reads this
        if(handSizeWasIncreased)
        {
            ReloadCards();
        }
        // If the player is just drawing cards but not increasing hand size needs the bonus
        else
        {
            currentHandSizeThisRound += amountOfCards;
            ReloadCards();
        }
    }

    /// <summary>
    /// Increases the hand size of the player and then draws new cards
    /// </summary>
    /// <param name="amountIncreased"> The number of cards increased </param>
    public void IncreaseHandSize(int amountIncreased)
    {
        if(handSize + amountIncreased >= maxHandSize)
        {
            handSize = maxHandSize;
            int drawAmount = 0;
            if(currentHandSizeThisRound + amountIncreased + 1 > maxHandSize)
            {
                drawAmount = currentHandSizeThisRound + amountIncreased + 1 - maxHandSize;
                currentHandSizeThisRound = maxHandSize;
                DrawCard(drawAmount, true);
            }
            else
            {
                currentHandSizeThisRound += amountIncreased + 1;
                DrawCard(amountIncreased + 1, true);
            }
        }
        else
        {
            handSize += amountIncreased;
            currentHandSizeThisRound += amountIncreased + 1;
            DrawCard(amountIncreased + 1, true);
        }
    }

    /// <summary>
    /// Rebuilds the cards in the scene when new cards are added (TODO: make it just add new ones and adjust the positions of old)
    /// </summary>
    public void ReloadCards()
    {
        // Destroying all of the old cards
        while(prefabedCards.Count > 0)
        {
            //Debug.Log($"Destroying {prefabedCards[0].name}");
            Destroy(prefabedCards[0]);
            prefabedCards.RemoveAt(0);
        }
        int xStartPoint = currentHandSizeThisRound - 1;
        // Returns the x position for where the first card should be placed
        float xPosition = -xStartPoint * cardSpacing / 2f;

        for(int i = 0; i < currentHandSizeThisRound; i++)
        {
            // Getting the position that the card will be shifted
            Vector3 position = new Vector3(xPosition + i * cardSpacing, 0f, 0f);
            // the card object being instantiated
            GameObject card = Instantiate(cardPrefab, handPosition);
            // Name for debugging purposes
            card.gameObject.name = playerHand[i].cardName + " " + i;
            // the position of the card being adjusted
            card.transform.localPosition = position;
            // Changing the text of the card prefab to reflect what it does
            card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerHand[i].cardName;
            card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerHand[i].description;
            // Setting up the button for the cards so that when they are select they perform their action
            Button button = card.GetComponent<Button>();
            Card currentCard = playerHand[i];
            // What the button does 1. Executes Card 2. Adds card to discard 3. Removes the card from player's hand 4. destroys the cards
            button.onClick.AddListener(() => 
            {
                discard.Add(currentCard);
                playerHand.Remove(currentCard);
                currentHandSizeThisRound--;
                prefabedCards.Remove(card);
                ReloadCards();
                CardEffectParser.Instance.ExecuteCard(currentCard);
                Destroy(card);
            });
            prefabedCards.Add(card);
        }
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

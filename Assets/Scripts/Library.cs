using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
    // Array of all possible cards
    public Card[] allCards;

    // List of player's chosen cards
    private List<Card> playerCards = new List<Card>();

    // Method to choose 3 random cards for player to use
    public List<Card> ChoosePlayerCards()
    {
        // Clear the list of player's chosen cards
        playerCards.Clear();

        // Randomly select 3 cards from the allCards array and add them to the playerCards list
        for (int i = 0; i < 3; i++)
        {
            Card cardToAdd = allCards[Random.Range(0, allCards.Length)];
            playerCards.Add(cardToAdd);
        }
        return playerCards;
    }
}

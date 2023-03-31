using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EncounterManager : MonoBehaviour
{
    [Header("Display")]
    public GameObject cardChoiceUI; // The UI object for displaying card choices
    [Header("CardDetails")]
    public TextMeshProUGUI[] cardNameTexts; // An array of TextMeshProUGUI objects for displaying card names
    public TextMeshProUGUI[] cardDescTexts; // An array of TextMeshProUGUI objects for displaying card descriptions
    public TextMeshProUGUI[] cardManaTexts; // An array of TextMeshProUGUI objects for display card mana cost
    public TextMeshProUGUI[] cardAttackValueText; // An array of TextMeshProUGUI objects for display of card attack
    public TextMeshProUGUI[] cardDefenseValueText; // An array of TextMeshProUGUI objects for display of card defense
    [Header("CardButtons")]
    public Button[] cardButtons; // An array of button objects for handling card effects
    [Header("PlayerCards")]
    private List<Card> playerCards = new List<Card>(); // The list of cards the player can choose from
    [Header("HealthDetails")]
    public TextMeshProUGUI enemyHealthText; // Enemy Health
    public TextMeshProUGUI playerHealthText; // An array of TextMeshProUGUI objects for displaying card descriptions
    public TextMeshProUGUI playerManaText; // Player Mana Ammount
    private int playerHealth;
    [SerializeField] private int playerMana;
    public int enemyCurrentHealth;
    [Header("References")]
    private PlayerController playerController;
    private EnemyController enemyController;
    private HoverTip hoverTip;
    [Header("TurnOrder")]
    //public bool isPlayerTurn = true;
    Turns turn;
    public bool isCombatOver;
    private bool possibleToPlay = true;
    [Header("CardRarityImages")]
    [SerializeField] private Sprite[] rarityImage;

    enum Turns
    {
        PlayerTurn,
        EnemyTurn
    }

    void Start()
    {

        // Turn off the card choice UI at the start
        cardChoiceUI.SetActive(false);
        // References
        playerController = FindObjectOfType<PlayerController>();
        hoverTip = FindObjectOfType<HoverTip>();
        // Temporary Player Health Show up
        playerHealth = playerController.playerHealth;
        playerMana = playerController.manaAmmount;
        playerHealthText.text = playerHealth.ToString();
    }
    private void Update()
    {
        //Debug.Log("PlayerTurn: " + isPlayerTurn);
        if (turn == Turns.EnemyTurn)
        {
            Combat();
        }

    }

    public void StartEncounter()
    {
        // Find the new enemy
        enemyController = FindObjectOfType<EnemyController>();
        // Display Enemy Health
        enemyCurrentHealth = enemyController.enemyMaxHealth;
        enemyController.enemyHealthText.text = enemyCurrentHealth.ToString();
        //enemyHealthText.text = enemyCurrentHealth.ToString();
        // Handle the encounter
        isCombatOver = false;
        int turnOrder = Random.Range(0, 1);
        
        switch (turnOrder)
        {
            case 0:
                turn = Turns.PlayerTurn;
                break;

            case 1:
                turn = Turns.EnemyTurn;
                break;

        }
        DisplayCardChoices();
    }

    // Displays all the card choices to the player
    public void DisplayCardChoices()
    {
        // Set Up Player Hand
        playerCards = FindObjectOfType<Library>().ChoosePlayerCards();
        cardChoiceUI.SetActive(true);
        int i = 0;
        int cardsToPlay = 0;
        possibleToPlay = true;
        foreach (Card card in playerCards)
        {
            cardNameTexts[i].text = card.cardName;
            cardDescTexts[i].text = card.description;
            cardManaTexts[i].text = card.manaCost.ToString();
            cardAttackValueText[i].text = card.attack.ToString();
            cardDefenseValueText[i].text = card.defense.ToString();
            switch (card.rarity)
            {
                case Rarity.Common:
                    cardButtons[i].GetComponent<Image>().sprite = rarityImage[0];
                    break;
                case Rarity.Rare:
                    cardButtons[i].GetComponent<Image>().sprite = rarityImage[1];
                    break;
                case Rarity.Epic:
                    cardButtons[i].GetComponent<Image>().sprite = rarityImage[2];
                    break;
                case Rarity.Legendary:
                    cardButtons[i].GetComponent<Image>().sprite = rarityImage[3];
                    break;
            }
            if(playerMana >= card.manaCost)
            {
                cardButtons[i].onClick.AddListener(() => PlayCard(card));
                cardsToPlay++;
            }
            if(cardsToPlay == 0)
            {
                possibleToPlay = false;
                Debug.Log(possibleToPlay);
            }
            i++;
        }
    }
    // Clears button listeners to not cause multiple listeners to overlap
    private void ClearButtonListeners()
    {
        int i = 0;
        foreach (Card card in playerCards)
        {
            cardButtons[i].onClick.RemoveAllListeners();
            i++;
        }
    }
    private void Combat()
    {
        Debug.Log("Combat: "+isCombatOver);
        if (isCombatOver)
        {
            return;
        }
        // Check if combat is over
        if (enemyCurrentHealth <= 0)
        {
            Debug.Log("Player wins!");
            EndCombat();
        }
        else
        {
            EnemyPlayCard();
        }
    }
    public void PlayCard(Card card)
    {

        if (turn == Turns.PlayerTurn && possibleToPlay)
        {
                // Player's turn
                switch (card.cardType)
                {
                    case CardType.Attack:
                        // Deal damage to enemy
                        int attackDamage = card.attack;
                        enemyCurrentHealth -= attackDamage;
                        enemyController.enemyHealthText.text = enemyCurrentHealth.ToString();
                        playerMana -= card.manaCost;
                        playerManaText.text = playerMana.ToString();
                        Debug.Log("Player deals " + attackDamage + " damage to enemy.");
                        break;
                    case CardType.Defense:
                        // Increase player's health
                        int defenseBonus = card.defense;
                        playerHealth += defenseBonus;
                        playerHealthText.text = playerHealth.ToString();
                        playerMana -= card.manaCost;
                        playerManaText.text = playerMana.ToString();
                    Debug.Log("Player increases their health by " + defenseBonus + ".");
                        break;
                    case CardType.Spell:
                        // Deal damage to enemy and increase player's health
                        int spellDamage = card.attack;
                        enemyCurrentHealth -= spellDamage;
                        enemyController.enemyHealthText.text = enemyCurrentHealth.ToString();
                        int spellHeal = Mathf.FloorToInt(spellDamage * 0.5f); // Heal for half the damage dealt
                        playerHealth += spellHeal;
                        playerHealthText.text = playerHealth.ToString();
                        playerMana -= card.manaCost;
                        playerManaText.text = playerMana.ToString();
                        Debug.Log("Player deals " + spellDamage + " damage to enemy and heals for " + spellHeal + ".");
                        break;
                }
            turn = Turns.EnemyTurn;
            ClearButtonListeners();
            DisplayCardChoices();

        }
    }

    private void EnemyPlayCard()
    {
            Card enemySelectedCard = new Card();
            enemySelectedCard = enemyController.PlayRandomCard();
            switch (enemySelectedCard.cardType)
            {
                case CardType.Attack:
                    // Deal damage to enemy
                    int attackDamage = enemySelectedCard.attack;
                    playerHealth -= attackDamage;
                    playerHealthText.text = playerHealth.ToString();
                    Debug.Log("Enemy deals " + attackDamage + " damage to player.");
                    break;
                case CardType.Defense:
                    // Increase player's health
                    int defenseBonus = enemySelectedCard.defense;
                    enemyCurrentHealth += defenseBonus;
                    enemyController.enemyHealthText.text = enemyCurrentHealth.ToString();
                    Debug.Log("Enemy increases their health by " + defenseBonus + ".");
                    break;
                case CardType.Spell:
                    // Deal damage to enemy and increase player's health
                    int spellDamage = enemySelectedCard.attack;
                    playerHealth -= spellDamage;
                    playerHealthText.text = playerHealth.ToString();
                    int spellHeal = Mathf.FloorToInt(spellDamage * 0.5f); // Heal for half the damage dealt
                    enemyCurrentHealth += spellHeal;
                    enemyController.enemyHealthText.text = enemyCurrentHealth.ToString();
                    Debug.Log("Enemy deals " + spellDamage + " damage to player and heals for " + spellHeal + ".");
                    break;
            }
        turn = Turns.PlayerTurn;

    }

    public void PassTurn()
    {
        turn = Turns.EnemyTurn;
        playerMana += 5;
        playerManaText.text = playerMana.ToString();
        ClearButtonListeners();
        DisplayCardChoices();
    }

    private void EndCombat()
    {
        enemyController.Die();
        ClearButtonListeners();
        cardChoiceUI.SetActive(false);
        isCombatOver = true;
    }
}

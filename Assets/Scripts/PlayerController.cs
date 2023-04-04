using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Details")]
    public float speed; // Player movement speed
    //public int playerHealth = 10;
    public int manaAmmount = 30;
    [Header("Encounter")]
    public bool isEncounter = false; // Whether the player is currently in an encounter
    public float encounterDistance; // Distance at which the player will encounter an enemy
    [Header("References")]
    private EncounterManager encounterManager;

    

    void Start()
    {
        isEncounter = false;
        encounterManager = FindObjectOfType<EncounterManager>();


    }

    void Update()
    {
        // Move the player continuously to the right
        Movement();
        // Check for an encounter
        CheckForEncounter();
    }

    void CheckForEncounter()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, encounterDistance);
        if (!isEncounter && hit.collider != null)
        {
            isEncounter = true;
            StopMovement();
            encounterManager.StartEncounter();
        }
        if (isEncounter && encounterManager.isCombatOver == true)
        {
            isEncounter = false;
            ResumeMovement();
        }
    }

    void Movement()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void StopMovement()
    {
        speed = 0;
    }
    
    void ResumeMovement()
    {
        speed = 5;
    }
}

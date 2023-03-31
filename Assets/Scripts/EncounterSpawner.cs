using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSpawner : MonoBehaviour
{
    public GameObject[] encounterPrefabs; // An array of possible encounters to spawn
    public float spawnIntervalMin; // The minimum time (in seconds) between encounter spawns
    public float spawnIntervalMax; // The maximum time (in seconds) between encounter spawns
    public float distanceBetweenEncounters; // The minimum distance (in units) between encounter spawns
    private Vector3 spawnPosition;
    private float nextSpawnTime; // The time at which the next encounter will spawn
    private PlayerController playerController;


    void Start()
    {
        // Set the time for the first encounter spawn
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        EnemySpawning();
    }

    public void EnemySpawning()
    {
        // Check if it's time to spawn an encounter
        if (Time.time >= nextSpawnTime && playerController.transform.position.x >= distanceBetweenEncounters)
        {
            if (!playerController.isEncounter)
            {
                // Choose a random encounter prefab from the array
                int randomIndex = Random.Range(0, encounterPrefabs.Length);
                GameObject encounterPrefab = encounterPrefabs[randomIndex];
                // Setup spawnPosition
                spawnPosition = new Vector3
                    (playerController.transform.position.x + distanceBetweenEncounters,
                    encounterPrefab.transform.position.y,
                    playerController.transform.position.z);
                // Instantiate the encounter at the spawner's position
                Instantiate(encounterPrefab, spawnPosition, Quaternion.identity);

                // Set the time for the next encounter spawn
                nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
            }
        }
    }
}

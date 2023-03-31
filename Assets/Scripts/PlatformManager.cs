using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab; // The platform prefab to use
    public float platformDistance; // Distance between platforms
    public int numPlatforms; // Number of platforms to generate at the start
    private List<GameObject> platforms = new List<GameObject>(); // List of platforms

    void Start()
    {
        // Generate initial platforms
        for (int i = 0; i < numPlatforms; i++)
        {
            CreatePlatform(new Vector2(i * platformDistance, 0));
        }
    }

    void Update()
    {
        // Check if the player has passed the first platform
        if (platforms[0].transform.position.x + platformDistance < Camera.main.transform.position.x)
        {
            // Move the first platform to the end and update the platforms list
            Vector2 newPos = new Vector2(platforms[platforms.Count - 1].transform.position.x + platformDistance, 0);
            platforms[0].transform.position = newPos;
            platforms.Add(platforms[0]);
            platforms.RemoveAt(0);
        }
    }

    void CreatePlatform(Vector2 position)
    {
        // Instantiate a new platform prefab at the given position
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity);
        platforms.Add(platform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spaceshipPrefab; // Spaceship prefab to mark the spawner position
    public GameObject[] enemyPrefabs; // Array to store different enemy types

    // Spawn region boundaries
    public float minX = -20f;
    public float maxX = 20f;
    public float minZ = -20f;
    public float maxZ = 20f;
    public float minDistance = 100f;


    private List<GameObject> spawnerSpaceships = new List<GameObject>(); // Store all spawner spaceships

    private void Start()
    {
        // Pre-calculate spawner positions and place spaceships
        PreCalculateSpawnerPositions();
    }

    private void PreCalculateSpawnerPositions()
    {
        // Generate a list of valid spawner positions
        for (int i = 0; i < 5; i++) // Adjust the number of spawners as needed
        {
            Vector3 spawnerPosition;

            // Find a valid position outside the inner square range
            do
            {
                spawnerPosition = new Vector3(
                    Random.Range(minX, maxX),
                    0,
                    Random.Range(minZ, maxZ)
                );
            }
            while (IsWithinInnerRange(spawnerPosition));

            // Place a visible spaceship at this spawner position and add to the list
            GameObject spaceship = Instantiate(spaceshipPrefab, spawnerPosition, Quaternion.identity);
            spawnerSpaceships.Add(spaceship);
        }
    }

    public void ReleaseEnemy(int enemyIndex)
    {
        if (enemyIndex < 0 || enemyIndex >= enemyPrefabs.Length)
        {
            Debug.LogWarning("Invalid enemy index!");
            return;
        }

        if (spawnerSpaceships.Count == 0)
        {
            Debug.LogWarning("No spawners available!");
            return;
        }

        // Pick a random spaceship (spawner)
        GameObject selectedSpaceship = spawnerSpaceships[Random.Range(0, spawnerSpaceships.Count)];

        // Spawn the enemy at the spaceship's position
        Vector3 spawnPosition = selectedSpaceship.transform.position + new Vector3(
            Random.Range(-1f, 1f), // Small random offset for variation
            0,
            Random.Range(-1f, 1f)
        );

        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
    }
    
    private bool IsWithinInnerRange(Vector3 position)
    {
        return Mathf.Abs(position.x) < minDistance && Mathf.Abs(position.z) < minDistance;
    }
}

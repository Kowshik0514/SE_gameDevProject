using UnityEngine;
using System.Collections.Generic;

public class Distorted3DGridFixedCount : MonoBehaviour
{
    public GameObject itemPrefab; // Assign your item prefab here
    public Vector3 gridCenter = Vector3.zero; // Center of the grid
    public float cellSize = 1f; // Distance between items
    public int gridSize = 14; // Total grid size (14x14)
    public int centerFixedSize = 2; // Fixed 2x2 block size
    public float maxZDistortion = 2f; // Maximum distortion in the Z-axis
    public float minDistance = 1.5f; // Minimum distance between items
    public int totalItems = 100; // Total number of items to place

    private HashSet<Vector3> occupiedCells = new HashSet<Vector3>();

    void Start()
    {
        int placedItems = 0;

        // Attempt to place items until the required count is reached
        while (placedItems < totalItems)
        {
            Vector3 randomPosition = GetRandomGridPosition();

            // Calculate scatter factor based on distance from the center
            float distanceFromCenter = new Vector2(randomPosition.x, randomPosition.z).magnitude;
            float scatterFactor = Mathf.Lerp(0f, maxZDistortion, distanceFromCenter / (gridSize / 2f));
            float distortedZ = randomPosition.z + Random.Range(-scatterFactor, scatterFactor);

            // Adjust density factor (denser near the center)
            float densityFactor = Mathf.Lerp(1f, 0.2f, distanceFromCenter / (gridSize / 2f));
            if (Random.value < densityFactor)
            {
                Vector3 distortedPos = new Vector3(randomPosition.x, gridCenter.y, distortedZ);
                if (TryPlaceItem(distortedPos))
                {
                    placedItems++;
                }
            }
        }
    }

    Vector3 GetRandomGridPosition()
    {
        // Generate a random position within the grid
        int x = Random.Range(-gridSize / 2, gridSize / 2 + 1);
        int z = Random.Range(-gridSize / 2, gridSize / 2 + 1);
        return new Vector3(x, gridCenter.y, z);
    }

    bool TryPlaceItem(Vector3 position)
    {
        // Snap position to grid for consistent placement
        Vector3 snappedPosition = new Vector3(
            Mathf.Round(position.x),
            position.y,
            Mathf.Round(position.z)
        );

        // Check if position is occupied or too close to existing items
        if (!IsPositionOccupied(snappedPosition))
        {
            Vector3 worldPosition = snappedPosition * cellSize + gridCenter;
            Instantiate(itemPrefab, worldPosition, Quaternion.identity);
            occupiedCells.Add(snappedPosition);
            return true; // Item successfully placed
        }
        return false; // Position is occupied
    }

    bool IsPositionOccupied(Vector3 position)
    {
        foreach (var occupied in occupiedCells)
        {
            if (Vector3.Distance(occupied, position) < minDistance / cellSize)
                return true;
        }
        return false;
    }
}

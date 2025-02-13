using UnityEngine;
using System.Collections;
public class RandomSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    public float minTime = 0.5f;       // Minimum time interval
    public float maxTime = 4f;       // Maximum time interval

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (true) // Loop indefinitely or based on a condition
        {
            float waitTime = Random.Range(minTime, maxTime); // Get a random time
            yield return new WaitForSeconds(waitTime);       // Wait for the time interval
            Instantiate(objectToSpawn, transform.position, Quaternion.identity); // Instantiate the object
        }
    }
}
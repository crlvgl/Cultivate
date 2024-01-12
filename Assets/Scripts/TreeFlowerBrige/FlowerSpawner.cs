using UnityEngine;
using System.Collections;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject flowerPrefab;
    public int numberOfFlowers = 15;
    public float waitingTimeMin = 2f;
    public float waitingTimeMax = 5f;

    void Start()
    {
        // Initial spawning of flowers
        StartCoroutine(SpawnFlowersWithDelay());
        for (int i = 0; i < 9; i++)
        {
            SpawnFlower();
        }
    }

    IEnumerator SpawnFlowersWithDelay()
    {

        for (int i = 0; i < numberOfFlowers -10; i++)
        {
            SpawnFlower();
            yield return new WaitForSeconds(Random.Range(waitingTimeMin,waitingTimeMax));
        }
    }

    public void SpawnNewFlowerAfterDelay()
    {
        StartCoroutine(SpawnFlowerWithDelay());
    }

    IEnumerator SpawnFlowerWithDelay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(Random.Range(waitingTimeMin,waitingTimeMax));

        // Spawn a new flower
        SpawnFlower();
    }

    // Method to spawn a single flower
    public void SpawnFlower()
    {
        Vector2 randomPosition = Random.insideUnitCircle * 1.5f;
        Vector3 spawnPosition = transform.position + new Vector3(randomPosition.x, randomPosition.y, 0);
        GameObject newFlower = Instantiate(flowerPrefab, spawnPosition, Quaternion.identity);
    }
}

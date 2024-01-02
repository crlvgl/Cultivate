using UnityEngine;
using System.Collections;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject flowerPrefab; 
    public int numberOfFlowers = 15;
    public float waitingTime = 3f;

    void Start()
    {
        // Initial spawning of flowers
        StartCoroutine(SpawnFlowersWithDelay());
    }

    IEnumerator SpawnFlowersWithDelay()
    {

        for (int i = 0; i < numberOfFlowers; i++)
        {
            SpawnFlower();
            yield return new WaitForSeconds(waitingTime);
        }
    }

    public void SpawnNewFlowerAfterDelay()
    {
        StartCoroutine(SpawnFlowerWithDelay());
    }

    IEnumerator SpawnFlowerWithDelay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(waitingTime);

        // Spawn a new flower
        SpawnFlower();
    }

    // Method to spawn a single flower
    private void SpawnFlower()
    {
        Vector2 randomPosition = Random.insideUnitCircle * 1.5f;
        Vector3 spawnPosition = transform.position + new Vector3(randomPosition.x, randomPosition.y, 0);
        GameObject newFlower = Instantiate(flowerPrefab, spawnPosition, Quaternion.identity);
    }
}

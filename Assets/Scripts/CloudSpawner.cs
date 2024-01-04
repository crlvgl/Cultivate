using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public int minTimeBetweenSpawns = 1;
    public int maxTimeBetweenSpawns = 10;
    public float spawnXPosition;
    public float spawnYPositionMin;
    public float spawnYPositionMax;
    // Start is called before the first frame update
    
    void Start()
    {
        StartCoroutine(SpawnCloudRandomly());
    }

    IEnumerator SpawnCloudRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
            SpawnCloud();
        }
    }
    
    private void SpawnCloud()
    {
        Vector2 randomPosition = new Vector2(spawnXPosition, Random.Range(spawnYPositionMin, spawnYPositionMax));
        GameObject newCloud = Instantiate(cloudPrefab, randomPosition, Quaternion.identity);
    }
}

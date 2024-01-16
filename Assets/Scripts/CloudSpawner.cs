using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public bool limitClouds = true;
    public int maxClouds = 20;
    public int minTimeBetweenSpawns = 1;
    public int maxTimeBetweenSpawns = 10;
    public float spawnXPosition;
    public float spawnYPositionMin;
    public float spawnYPositionMax;
    public static int cloudCount = 0;
    // Start is called before the first frame update
    
    void Start()
    {
        if (limitClouds == false)
        {
            maxClouds = 1000000;
        }
        StartCoroutine(SpawnCloudRandomly());
    }

    IEnumerator SpawnCloudRandomly()
    {
            if (cloudCount <= maxClouds) // if max clouds is reached, Unity crashes !! needs to be fixed !!
            {
                yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
                SpawnCloud();
            }
    }
    
    private void SpawnCloud()
    {
        Vector2 randomPosition = new Vector2(spawnXPosition, Random.Range(spawnYPositionMin, spawnYPositionMax));
        GameObject newCloud = Instantiate(cloudPrefab, randomPosition, Quaternion.identity);
        cloudCount++;
    }
}

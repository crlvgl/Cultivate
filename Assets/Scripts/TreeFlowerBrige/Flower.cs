using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public Transform playerTransform;
    public FlowerSpawner flowerSpawner;
    public static bool flowerExhausted = false;
    private int increaseFlower;
    private GameObject sparkle;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesHitTriggers = true;

        if (this.GetComponent<Collider2D>().isActiveAndEnabled == false)
        {
            this.GetComponent<Collider2D>().enabled = true;
        }

        if (DevMode.devMode == true)
        {
            increaseFlower = 10;
        }
        else
        {
            increaseFlower = 1;
        }
        
        GameObject playerGameObject = GameObject.FindWithTag("Player");
        if (playerGameObject != null)
        {
            playerTransform = playerGameObject.transform;
        }

        GameObject spawnerGameObject = GameObject.Find("ZoomZone_flower_meadow");
        if (spawnerGameObject != null)
        {
            flowerSpawner = spawnerGameObject.GetComponent<FlowerSpawner>();
        }
        sparkle = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerCloseToTheObject(1f) && Inventory.Altar != 0 && flowerExhausted == false)
        {
            if (sparkle != null)
            {
                sparkle.SetActive(true);
            }

            if(IsPlayerCloseToTheObject(0.2f)) 
            {
                CollectFlower();
            }
        }
        else
        {
            if (sparkle != null)
            {
                sparkle.SetActive(false);
            }
        }
    }
    
    void CollectFlower()
    {
        Destroy(gameObject);  // Destroy the flower game object
        Inventory.Flower += increaseFlower;
        Exhaustion.flowersPicked += 1;

        if (flowerSpawner != null)
        {
            flowerSpawner.SpawnNewFlowerAfterDelay();
        }
    }

    bool IsPlayerCloseToTheObject(float distanceForSparkle)
    {
        // Get the center of the object
        Vector2 objectCenter = new Vector2(this.transform.position.x, this.transform.position.y);
        float distance = Vector2.Distance(playerTransform.position, objectCenter);
        return distance <= distanceForSparkle;
    }
}

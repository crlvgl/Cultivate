using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public Transform playerTransform;
    public FlowerSpawner flowerSpawner;
    public static bool flowerExhausted = false;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerCloseToTheObject() == true) 
        {
            if (flowerExhausted == false)
            {
                CollectFlower();
            }
            else if (flowerExhausted == true)
            {
                Debug.Log("Exhausted, can't pick anymore");
            }
        }
    }
    
    void CollectFlower()
    {
        Destroy(gameObject);  // Destroy the flower game object
        Inventory.Flower += 1;
        Exhaustion.flowersPicked += 1;

        if (flowerSpawner != null)
        {
            flowerSpawner.SpawnNewFlowerAfterDelay();
        }
    }




    bool IsPlayerCloseToTheObject()
    {
        // Get the center of the object
        Vector2 objectCenter = new Vector2(this.transform.position.x, this.transform.position.y);
        float distance = Vector2.Distance(playerTransform.position, objectCenter);
        return distance <= 0.2f;
    }
}

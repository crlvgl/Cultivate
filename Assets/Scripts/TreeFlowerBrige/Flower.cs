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
        sparkle =this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerCloseToTheObject() && Inventory.Altar != 0)
        {
            if (sparkle != null)
            {
                sparkle.SetActive(true);
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

    void OnMouseDown()
    {
        Debug.Log(this.name + "MouseDown");
        if (flowerExhausted == false && Inventory.Altar != 0)
        {
            if (IsPlayerCloseToTheObject() == true) // to make sure only one tree at a time is klicked
            {
                CollectFlower();
            }
        }
        else if (flowerExhausted == true)
        {
            Debug.Log("Exhausted, can't pick anymore");
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

    bool IsPlayerCloseToTheObject()
    {
        // Get the center of the object
        Vector2 objectCenter = new Vector2(this.transform.position.x, this.transform.position.y);
        float distance = Vector2.Distance(playerTransform.position, objectCenter);
        return distance <= 0.2f;
    }
}

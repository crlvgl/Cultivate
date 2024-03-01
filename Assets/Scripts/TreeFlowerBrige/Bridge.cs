using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{

    public Transform playerTransform;
    public Transform brokenBridge1Transform;
    public GameObject brokenBridge1;
    public GameObject repairedBridge1;
    public GameObject player_0;
    public GameObject player_1;
    public int CostBridge1 = 10;
    public static bool Bridge1Unlocked = false;
    public FlowerSpawner flowerSpawner;
    private GameObject sparkle;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnerGameObject = GameObject.Find("ZoomZone_flower_meadow");
        if (spawnerGameObject != null)
        {
            flowerSpawner = spawnerGameObject.GetComponent<FlowerSpawner>();
        }
        sparkle =this.transform.GetChild(0).gameObject;
    }

    bool IsPlayerCloseToTheObject()
    {
        float distance = Vector2.Distance(playerTransform.position, brokenBridge1Transform.position);
        return distance <= 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerCloseToTheObject() && Inventory.Relic != 0 && Inventory.Wood >= CostBridge1)
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

        if (Bridge1Unlocked == true)
        {
            RepairBridge1(); //Must be checked separately if a save is loaded with Bridge1Unlocked = true
        }
    }


    void OnMouseDown()
    {
        if (Inventory.Wood >= CostBridge1 && IsPlayerCloseToTheObject())
        {
            Bridge1Unlocked = true;
            Debug.Log("Woohoo unlocked");
            Inventory.Wood -= CostBridge1;
        }
        else
        {
            Debug.Log("You need more Wood");
        }
    }

    void RepairBridge1()
    {
        //Unlock area 2
        brokenBridge1.SetActive(false);
        repairedBridge1.SetActive(true);
        player_0.SetActive(false);
        player_1.SetActive(true);
        player_1.transform.position = player_0.transform.position;
        ResetFlowers();
        DevMode.activateSprintV2 = true;
    }

    void ResetFlowers()
    {
        foreach (GameObject flower in GameObject.FindGameObjectsWithTag("Flower"))
        {
            Destroy(flower);
        }
        for (int i = 0; i < flowerSpawner.numberOfFlowers+1; i++)
        {
            flowerSpawner.SpawnFlower();
        }
    }
}

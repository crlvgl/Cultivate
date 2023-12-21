using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{

    public Transform Player;
    public GameObject RelicGO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.Relic == 0)
        {
            RelicGO.SetActive(true);
        }
        else
        {
            RelicGO.SetActive(false);
        }

        if (IsPlayerCloseToRelic())
        {
            RelicGO.SetActive(false);
            Inventory.Relic = 1;
        }
    }

        
    bool IsPlayerCloseToRelic()
    {
        float distance = Vector2.Distance(Player.transform.position, this.transform.position);
        return distance <= 0.5f;
    }
}

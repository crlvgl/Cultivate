using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{

    public GameObject Player1;
    public GameObject Player2;
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
        if (Player1.activeSelf)
        {
            float distance = Vector2.Distance(Player1.transform.position, this.transform.position);
            return distance <= 0.5f;
        }
        else if (Player2.activeSelf)
        {
            float distance = Vector2.Distance(Player2.transform.position, this.transform.position);
            return distance <= 0.5f;
        }
        else
        {
            return false;
        }
    }
}

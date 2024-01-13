using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{

    public GameObject Player1;
    public GameObject Player2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerCloseToAltar())
        {
            Inventory.Altar = 1;
        }
    }

    bool IsPlayerCloseToAltar()
    {
        if (Player1.activeSelf)
        {
            float distance = Vector2.Distance(Player1.transform.position, this.transform.position);
            return distance <= 1f;
        }
        else if (Player2.activeSelf)
        {
            float distance = Vector2.Distance(Player2.transform.position, this.transform.position);
            return distance <= 1f;
        }
        else
        {
            return false;
        }
    }
}

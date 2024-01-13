using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.Label(new Rect(900, 30, 300, 200), "Wood: " + Inventory.Wood.ToString() + "  Flower: " + Inventory.Flower.ToString());
        if (Bridge.Bridge1Unlocked == true)
        {
            GUI.Label(new Rect(900, 45, 300, 200), "Bridge1 Unlocked");
        }
        else
        {
            GUI.Label(new Rect(900, 45, 300, 200), "Bridge1 Locked");
        }
        if (Inventory.Relic == 1)
        {
            GUI.Label(new Rect(900, 60, 300, 200), "Woohoo Relic");
        }
        if (Inventory.Altar == 1)
        {
            GUI.Label(new Rect(900, 75, 300, 200), "Altar unlocked");
        }
        GUI.Label(new Rect(900, 90, 300, 200), "exhaustionPoints: " + Exhaustion.exhaustionPoints);
        
    }
}

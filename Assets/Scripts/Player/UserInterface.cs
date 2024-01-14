using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    int windowHeight = 200;
    int windowWidth = 300;
    int windowLeftAlignment;
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
        windowLeftAlignment = (Screen.width / 2) - (windowWidth / 2);
        GUI.Label(new Rect(windowLeftAlignment, 30, windowWidth, windowHeight), "Wood: " + Inventory.Wood.ToString() + "  Flower: " + Inventory.Flower.ToString());
        if (Bridge.Bridge1Unlocked == true)
        {
            GUI.Label(new Rect(windowLeftAlignment, 45, windowWidth, windowHeight), "Bridge1 Unlocked");
        }
        else
        {
            GUI.Label(new Rect(windowLeftAlignment, 45, windowWidth, windowHeight), "Bridge1 Locked");
        }
        if (Inventory.Relic == 1)
        {
            GUI.Label(new Rect(windowLeftAlignment, 60, windowWidth, windowHeight), "Woohoo Relic");
        }
        if (Inventory.Altar == 1)
        {
            GUI.Label(new Rect(windowLeftAlignment, 75, windowWidth, windowHeight), "Altar unlocked");
        }
        GUI.Label(new Rect(windowLeftAlignment, 90, windowWidth, windowHeight), "exhaustionPoints: " + Exhaustion.exhaustionPoints);
        
    }
}

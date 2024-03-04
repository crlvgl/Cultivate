using UnityEngine;
using UnityEngine.UI; // Required for working with UI

public class Sacrifice : MonoBehaviour
{
    public Transform player2Transform;
    public Button Button1; 
    public Button Button2;
    private GameObject button1;
    private GameObject button2;

    void Start()
    {
        // Ensure there's a Button component attached to the GameObject that this script is attached to
        if (Button1 == null) Button1 = GetComponent<Button>();
        if (Button2 == null) Button2 = GetComponent<Button>();
        // Add a listener to the button to execute the UseWood method when it's clicked
        Button1.onClick.AddListener(UseButton1);
        Button2.onClick.AddListener(UseButton2);
        button1 = transform.Find("SacrificeButton1").gameObject;
        button2 = transform.Find("SacrificeButton2").gameObject;
        button1.SetActive(false);
        button2.SetActive(false);
    }

    void Update()
    {
        // Check if the player is close enough to the object
        if (IsPlayerCloseToTheObject())
        {
            // Show Button1 if Inventory.Pickaxe == 0, otherwise show Button2
            if (Inventory.Wood >= 10 && Inventory.Flower >=10 && Inventory.Pickaxe == 0)
            {
                button1.SetActive(true);
                button2.SetActive(false);
            }
            else if (Inventory.Wood >= 10 && Inventory.Stone >=10 && Inventory.Pickaxe == 1 && Inventory.Relic == 1)
            {
                button1.SetActive(false);
                button2.SetActive(true);
            }
        }
        else
        {
            // Player is not close enough, deactivate both buttons
            button1.SetActive(false);
            button2.SetActive(false);
        }
    }

    void UseButton1()
    {
        // Check if there's enough wood
        if (Inventory.Wood >= 10 && Inventory.Flower >=10 && Inventory.Pickaxe == 0)
        {
            Debug.Log("Was: Wood = " + Inventory.Wood + "; Flower = " + Inventory.Flower + "; Pickaxe = "+ Inventory.Pickaxe);
            Inventory.Wood -= 10;
            Inventory.Flower -= 10;
            Inventory.Pickaxe = 1;
            Debug.Log("Now: Wood = " + Inventory.Wood + "; Flower = " + Inventory.Flower + "; Pickaxe = "+ Inventory.Pickaxe);
            button1.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough ressources.");
        }
    }
    

    void UseButton2()
    {
        if (Inventory.Wood >= 10 && Inventory.Stone >=10 && Inventory.Pickaxe == 1)
        {
            Debug.Log("Was: Wood = " + Inventory.Wood + "; Stone = " + Inventory.Stone);
            Inventory.Wood -= 10;
            Inventory.Stone -= 10;
            Inventory.Relic = 2;
            Debug.Log("Now: Wood = " + Inventory.Wood + "; Stone = " + Inventory.Stone);
            button2.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough ressources.");
        }
    }

    bool IsPlayerCloseToTheObject()
    {
        // Get the center of the object
        Vector2 objectCenter = new Vector2(this.transform.position.x, this.transform.position.y);
        float distance = Vector2.Distance(player2Transform.position, objectCenter);
        return distance <= 1.0f;
    }
}

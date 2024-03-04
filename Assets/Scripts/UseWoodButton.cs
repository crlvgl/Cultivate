using UnityEngine;
using UnityEngine.UI; // Required for working with UI

public class UseWoodButton : MonoBehaviour
{
    public Button yourButton; // Assign this in the inspector

    void Start()
    {
        // Ensure there's a Button component attached to the GameObject that this script is attached to
        if (yourButton == null) yourButton = GetComponent<Button>();

        // Add a listener to the button to execute the UseWood method when it's clicked
        yourButton.onClick.AddListener(UseWood);
    }

    void UseWood()
    {
        // Check if there's enough wood
        if (Inventory.Wood >= 10)
        {
            Inventory.Wood -= 10;
            Debug.Log("Used 10 wood. Remaining wood: " + Inventory.Wood);
        }
        else
        {
            Debug.Log("Not enough wood.");
        }
    }
}

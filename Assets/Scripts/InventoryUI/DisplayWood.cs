using UnityEngine;
using TMPro;

public class DisplayWood : MonoBehaviour
{
    TMP_Text WoodText; 

    void Start()
    {
        WoodText = GetComponent<TMP_Text>();
        if (WoodText == null)
        {
            Debug.LogError("unable to find a TMP_Text component on the GameObject.");
        }
    }

    void Update()
    {
        if (WoodText != null)
        {
            WoodText.text = Inventory.Wood.ToString();
        }
    }
}

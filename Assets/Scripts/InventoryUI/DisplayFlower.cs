using UnityEngine;
using TMPro;

public class DisplayFlower : MonoBehaviour
{
    TMP_Text FlowerText; 

    void Start()
    {
        FlowerText = GetComponent<TMP_Text>();
        if (FlowerText == null)
        {
            Debug.LogError("unable to find a TMP_Text component on the GameObject.");
        }
    }

    void Update()
    {
        if (FlowerText != null)
        {
            FlowerText.text = Inventory.Flower.ToString();
        }
    }
}

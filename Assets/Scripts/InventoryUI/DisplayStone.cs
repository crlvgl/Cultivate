using UnityEngine;
using TMPro;

public class DisplayStone : MonoBehaviour
{
    TMP_Text StoneText; 

    void Start()
    {
        StoneText = GetComponent<TMP_Text>();
        if (StoneText == null)
        {
            Debug.LogError("unable to find a TMP_Text component on the GameObject.");
        }
    }

    void Update()
    {
        if (StoneText != null)
        {
            StoneText.text = Inventory.Stone.ToString();
        }
    }
}

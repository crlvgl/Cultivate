using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private GameObject pickaxe;
    private GameObject axe;

    void Start()
    {
        pickaxe = transform.Find("Pickaxe").gameObject;

        if (pickaxe != null)
        {
            pickaxe.SetActive(false);
        }
        else
        {
            Debug.LogError("Pickaxe not found!");
        }

        axe = transform.Find("Axe").gameObject;

        if (axe != null)
        {
            axe.SetActive(false);
        }
        else
        {
            Debug.LogError("Axe not found!");
        }
    }

    void Update()
    {
        if (Inventory.Pickaxe == 1 && pickaxe != null)
        {
            pickaxe.SetActive(true); 
        }

        if (Inventory.Relic == 1 && axe != null)
        {
            axe.SetActive(true);
        }
    }
}

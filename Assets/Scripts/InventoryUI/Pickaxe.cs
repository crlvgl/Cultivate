using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private GameObject pickaxe; 

    void Start()
    {
        pickaxe = transform.Find("Pickaxe").gameObject;

        if (pickaxe != null)
        {
            pickaxe.SetActive(false);
        }
    }

    void Update()
    {
        if (Inventory.Pickaxe == 1 && pickaxe != null)
        {
            pickaxe.SetActive(true); 
        }
    }
}

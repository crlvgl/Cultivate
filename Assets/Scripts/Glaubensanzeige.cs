using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glaubensanzeige : MonoBehaviour
{
    private GameObject imageRelic1;
    private GameObject imageRelic3;
    private GameObject imageRelic4;
    
    // Start is called before the first frame update
    void Start()
    {
        imageRelic1 = transform.Find("RelicUI/ImageRelic1").gameObject;
        imageRelic3 = transform.Find("RelicUI/ImageRelic3").gameObject;
        imageRelic4 = transform.Find("RelicUI/ImageRelic4").gameObject;
        if (imageRelic1 != null)
        {
            imageRelic1.SetActive(false);
        }
        if (imageRelic3 != null)
        {
            imageRelic3.SetActive(false);
        }
        if (imageRelic4 != null)
        {
            imageRelic4.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Inventory.Relic == 1)
        {
            imageRelic1.SetActive(true);
        }
        if(Inventory.Altar == 1)
        {
            imageRelic1.SetActive(false);
            imageRelic3.SetActive(true);
        }
        if(Inventory.Relic == 2)
        {
            imageRelic1.SetActive(false);
            imageRelic3.SetActive(false);
            imageRelic4.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glaubensanzeige : MonoBehaviour
{
    private GameObject imageRelic;
    
    // Start is called before the first frame update
    void Start()
    {
        imageRelic = transform.Find("RelicUI/ImageRelic").gameObject;
        if (imageRelic != null)
        {
            imageRelic.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Inventory.Relic == 1)
        {
            imageRelic.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillars : MonoBehaviour
{
    private GameObject PillarOld;
    private GameObject PillarNew;

    // Start is called before the first frame update
    void Start()
    {
        PillarOld = this.transform.Find("Pillars Old").gameObject;
        PillarNew = this.transform.Find("Pillars New").gameObject;

        PillarOld.SetActive(true);
        PillarNew.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.Relic == 2)
        {
            PillarOld.SetActive(false);
            PillarNew.SetActive(true);
        }
    }
}

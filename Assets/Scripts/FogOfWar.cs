using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public int phase = 1;
    private bool destroyOnPhase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPhase();

        if(destroyOnPhase == true)
        {
            Destroy(gameObject);
        }
    }

    void checkPhase()
    {
        if (phase == 1)
        {
            destroyOnPhase = Bridge.Bridge1Unlocked;
        }
        else if (phase == 2)
        {
            // add when condition for phase 2 exists
        }
        else if (phase == 3)
        {
            // add when condition for phase 3 exists
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndExit : MonoBehaviour
{
    public GameObject GreyStuff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && EndGame.disableSaveMenu == false)
        {
            if (GreyStuff.activeSelf == false)
            {
                GreyStuff.SetActive(true);
                EndGame.disableAll = true;
            }
            else
            {
                GreyStuff.SetActive(false);
                EndGame.disableAll = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowDialoguePopulator : MonoBehaviour
{
    [Tooltip("Number in the Hirarchy; when reached, the Dialogue is used; MUST BE UNIQUE FOR EACH DIALOGUE!")]
    public int NumInHirarchy;

    [Tooltip("The Dialogue of the Crow; each Sentence is written on one Slide")]
    [TextArea(3, 10)]
    public string[] crowTalks;

    private bool copied = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (copied == false)
        {
            if (CrowTalk.TapNum == NumInHirarchy)
            {
                copied = true;
                CrowTalk.crowTalks = crowTalks;
            }
        }
        else if (copied == true)
        {
            if (CrowTalk.TapNum != NumInHirarchy)
            {
                copied = false;
            }
        }
    }

}

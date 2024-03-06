using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowDialoguePopulator : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [Tooltip("If true, the Dialogue is triggered by clicking on the Crow")]
    public bool onClick;
    [Tooltip("If true, the Dialogue is triggered by the script")]
    public bool scripted;
    [Tooltip("Number in the Hirarchy; when reached, the Dialogue is used; MUST BE UNIQUE FOR EACH DIALOGUE!")]
    public int NumInHirarchy;
    [Tooltip("The Trigger for the Dialogue; must be unique for each Dialogue, set by CrowTalk.cs; [start, tree, wood, altar, sacrificeFlower, pickaxe, sacrificeStone]")]
    public string trigger;

    [Tooltip("The Dialogue of the Crow; each Sentence is written on one Slide")]
    [TextArea(3, 10)]
    public string[] crowTalks;

    private bool copied = false;

    // Start is called before the first frame update
    void Start()
    {
        if (onClick == false && scripted == false)
        {
            Debug.LogError("Please set either onClick or scripted to true");
        }
        else if (onClick == true && scripted == true)
        {
            Debug.LogError("Please set only one of onClick or scripted to true");
        }
    }

    void Update()
    {
        if (onClick)
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
        else if (scripted)
        {
            if (CrowTalk.scriptTrigger == trigger)
            {
                CrowTalk.crowTalksScript = crowTalks;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowTalk : MonoBehaviour
{
    public static string[] crowTalks;
    public static string[] crowTalksScript;
    public static string[] crowTalksExhaustion;
    [Tooltip("Exhaustionpoints remaining to trigger Dialogue about exhaustion; 0-100")]
    public int exhaustionTrigger;
    private static Queue<string> crowTalkQueue;
    public static GameObject TextBox;
    [Tooltip("Total Number of Hirarchy; must be the same as the Total Number of unique Dialogues")]
    public int HirarchyNum;
    public static string scriptTrigger = "start"; // triggers scripted Dialogue
    [Tooltip("Time to wait until the first Dialogue is read, in seconds")]
    public float waitTilFirstDialogue;
    private bool hasStarted = false;
    [Tooltip("Number of Dialogues to only read once; will remove the first x Dialogues from the Queue; cant be higher than the Total Number of Dialogues")]
    public int RepeatOnce;
    public static int TapNum = 1;
    private static TMP_Text TextBoxText;
    private static string tempText;
    private bool didTalk = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        crowTalkQueue = new Queue<string>();

        TextBox = this.transform.Find("TextBox").gameObject;
        TextBoxText = TextBox.transform.Find("Dialogue").GetComponent<TMP_Text>();

        if (RepeatOnce >= HirarchyNum)
        {
            Debug.LogError("RepeatOnce cant be higher than HirarchyNum");
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (EndGame.disableAll == true)
        {
            return;
        }
        if (scriptTrigger == "start")
        {
            if (TextBox.activeSelf == false && hasStarted == false)
            {
                StartCoroutine(StartTalkingScriptOne());
            }
        }
        else if (scriptTrigger == "tree" && Inventory.Relic == 1)
        {
            if (TextBox.activeSelf == false)
            {
                TextBox.SetActive(true);
                StartTalkingScript();
                scriptTrigger = "wood";
            }
        }
        else if (scriptTrigger == "wood" && Inventory.Wood == 10)
        {
            if (TextBox.activeSelf == false)
            {
                TextBox.SetActive(true);
                StartTalkingScript();
                scriptTrigger = "altar";
            }
        }
        else if (scriptTrigger == "altar" && Inventory.Altar == 1)
        {
            if (TextBox.activeSelf == false)
            {
                TextBox.SetActive(true);
                StartTalkingScript();
                scriptTrigger = "sacrificeFlower";
            }
        }
        else if (scriptTrigger == "sacrificeFlower" && Inventory.Flower == 50)
        {
            if (TextBox.activeSelf == false)
            {
                TextBox.SetActive(true);
                StartTalkingScript();
                scriptTrigger = "pickaxe";
            }
        }
        else if (scriptTrigger == "pickaxe" && Inventory.Pickaxe == 1)
        {
            if (TextBox.activeSelf == false)
            {
                TextBox.SetActive(true);
                StartTalkingScript();
                scriptTrigger = "sacrificeStone";
            }
        }
        else if (scriptTrigger == "sacrificeStone" && Inventory.Stone == 10)
        {
            if (TextBox.activeSelf == false)
            {
                TextBox.SetActive(true);
                StartTalkingScript();
                scriptTrigger = "";
            }
        }
        if (TextBox.activeSelf == false)
        {
            if (Exhaustion.exhaustionPoints <= exhaustionTrigger && didTalk == false)
            {
                TextBox.SetActive(true);
                didTalk = true;
                StartTalkingExhaustion();
            }
        }
    }

    void OnMouseDown()
    {
        if (EndGame.disableAll == true)
        {
            return;
        }
        if (TextBox.activeSelf == false)
        {
            TextBox.SetActive(true);
            if (TapNum >= HirarchyNum)
            {TapNum = RepeatOnce;}
            TapNum++;
            StartTalking();
        }
    }

    void StartTalking()
    {
        crowTalkQueue.Clear();
        
        foreach (string talk in crowTalks)
        {
            crowTalkQueue.Enqueue(talk);
        }

        audioSource.Play(); 
        ContinueTalking();
    }

    void StartTalkingExhaustion()
    {
        crowTalkQueue.Clear();
        
        foreach (string talk in crowTalksExhaustion)
        {
            crowTalkQueue.Enqueue(talk);
        }

        audioSource.Play(); 
        ContinueTalking();
    }

    void StartTalkingScript()
    {
        crowTalkQueue.Clear();
        
        foreach (string talk in crowTalksScript)
        {
            crowTalkQueue.Enqueue(talk);
        }

        audioSource.Play(); 
        ContinueTalking();
    }

    IEnumerator StartTalkingScriptOne()
    {
        hasStarted = true;
        yield return new WaitForSeconds(waitTilFirstDialogue);
        TextBox.SetActive(true);
        StartTalkingScript();
        scriptTrigger = "tree";
    }

    public static void ContinueTalking()
    {
        if (crowTalkQueue.Count == 0)
        {
            EndDialogue();
            return;
        }
        TextBoxText.text = crowTalkQueue.Dequeue();
    }

    static void EndDialogue()
    {
        TextBox.SetActive(false);
        TextBoxText.text = "";
    }
}

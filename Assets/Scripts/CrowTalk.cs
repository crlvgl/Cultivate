using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowTalk : MonoBehaviour
{
    public static string[] crowTalks;
    private static Queue<string> crowTalkQueue;
    public static GameObject TextBox;
    [Tooltip("Total Number of Hirarchy; must be the same as the Total Number of unique Dialogues")]
    public int HirarchyNum;
    [Tooltip("Number of Dialogues to only read once; will remove the first x Dialogues from the Queue; cant be higher than the Total Number of Dialogues")]
    public int RepeatOnce;
    public static int TapNum = 1;
    private static TMP_Text TextBoxText;
    private static string tempText;

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
    }

    void OnMouseDown()
    {
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

        ContinueTalking();
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

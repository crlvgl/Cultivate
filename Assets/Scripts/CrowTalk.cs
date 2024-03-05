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
    public static int TapNum = 1;
    private static TMP_Text TextBoxText;
    private static string tempText;

    // Start is called before the first frame update
    void Start()
    {
        crowTalkQueue = new Queue<string>();

        TextBox = this.transform.Find("TextBox").gameObject;
        TextBoxText = TextBox.transform.Find("Dialogue").GetComponent<TMP_Text>();
    }

    void OnMouseDown()
    {
        if (TextBox.activeSelf == false)
        {
            TextBox.SetActive(true);
            if (TapNum >= HirarchyNum)
            {TapNum = 0;}
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
        tempText = crowTalkQueue.Dequeue();
        TextBoxText.text = tempText;
        Debug.Log(tempText);
    }

    static void EndDialogue()
    {
        TextBox.SetActive(false);
        TextBoxText.text = "";
    }
}

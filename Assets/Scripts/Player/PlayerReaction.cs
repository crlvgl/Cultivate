using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReaction : MonoBehaviour
{

    private float distance;
    private float walkingDistance;
    private GameObject questionMark;
    private GameObject exclamationMark;

    public int popupTimerSetting = 1;
    public static float popupTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (DevMode.devMode == true)
        {
            popupTimer = 0;
        }
        else
        {
            popupTimer = popupTimerSetting;
        }
        
        questionMark = this.transform.Find("QuestionMark").gameObject;
        exclamationMark = this.transform.Find("ExclamationMark").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (EndGame.disableAll == true)
        {
            return;
        }
        if (ClickAgentController.PlayerControl != 4)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && DistancePlayerMouse() > WalkingDistance())
            {
                StartCoroutine(QuestionTimer());
                // Debug.Log("Question");
            }
            else if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && DistancePlayerMouse() < WalkingDistance())
            {
                if (TreeAnimation.wantsToChop == true && (TreeAnimation.chopExhausted == true || Inventory.Relic == 0))
                {
                    StartCoroutine(QuestionTimer());
                }
                else
                {
                    StartCoroutine(ExclamationTimer());
                }
                // Debug.Log("Exclamation");
            }
        }
        else if (ClickAgentController.PlayerControl == 4 && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            if (TreeAnimation.wantsToChop == true && (TreeAnimation.chopExhausted == true || Inventory.Relic == 0))
            {
                StartCoroutine(QuestionTimer());
            }
            else
            {
                StartCoroutine(ExclamationTimer());
            }
            // Debug.Log("Exclamation");
        }
    }

    float DistancePlayerMouse()
    {
        distance = Vector2.Distance(this.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        return distance;
    }

    float WalkingDistance()
    {
        if (ClickAgentController.PlayerControl == 1)
        {
            walkingDistance = ClickAgentController.walkingDistance1;
        }

        else if (ClickAgentController.PlayerControl == 2)
        {
            walkingDistance = ClickAgentController.walkingDistance2;
        }

        else if (ClickAgentController.PlayerControl == 3)
        {
            walkingDistance = ClickAgentController.walkingDistance3;
        }
        else
        {
            walkingDistance = 0;
        }

        return walkingDistance;
    }

    IEnumerator QuestionTimer()
    {
        questionMark.SetActive(true);
        yield return new WaitForSeconds(popupTimer);
        questionMark.SetActive(false);
    }

    IEnumerator ExclamationTimer()
    {
        exclamationMark.SetActive(true);
        yield return new WaitForSeconds(popupTimer);
        exclamationMark.SetActive(false);
    }
}

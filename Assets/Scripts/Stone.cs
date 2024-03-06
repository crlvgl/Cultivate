using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public static float WaitSecondsStone = 3f;
    private bool sparkleOff = false;
    private GameObject sparkle;
    private StoneProgressBar StoneProgressBar;
    private GameObject progressBarSlider;
    public bool StoneTimer = false;
    public GameObject player2;
    private int increaseStone;


    void Start()
    {
        if (DevMode.devMode == true)
        {
            WaitSecondsStone = 1;
            increaseStone = 5;
        }
        else
        {
            increaseStone = 1;
        }
        StoneProgressBar = GetComponentInChildren<StoneProgressBar>();
        progressBarSlider = this.transform.GetChild(0).gameObject;
        sparkle =this.transform.GetChild(1).gameObject;
        DeactivateProgressBar();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerCloseToTheObject() && Inventory.Pickaxe != 0)
        {
            if (sparkle != null && sparkleOff == false)
            {
                sparkle.SetActive(true);
            }
        }
        else
        {
            if (sparkle != null)
            {
                sparkle.SetActive(false);
            }
        }
    }

    void OnMouseDown()
    {
        if (Inventory.Pickaxe == 1)
        {
            if (ClickAgentController.holdStill == false && IsPlayerCloseToTheObject() == true) 
            {
                // Start the coroutine to handle the animation
                StartCoroutine(PlayAnimation());
                StartCoroutine(CollectStone());
                StartCoroutine(ProgressBar());
            }
        }
    }

    IEnumerator PlayAnimation()
    {
        ClickAgentController.holdStill = true;
        sparkle.SetActive(false);
        sparkleOff = true;

        // Wait for 3 seconds
        yield return new WaitForSeconds(WaitSecondsStone);

        ClickAgentController.holdStill = false;
        sparkleOff = false;
    }

    IEnumerator ProgressBar()
    {
        if (StoneProgressBar != null)
        {
            progressBarSlider.SetActive(true);
            StoneProgressBar.Progress(1f);
            yield return new WaitForSeconds(WaitSecondsStone);
            progressBarSlider.SetActive(false);
            StoneProgressBar.resetProgress();
        }
    }

    public void DeactivateProgressBar()
    {
        progressBarSlider.SetActive(false);
        StoneProgressBar.resetProgress();
    }

    IEnumerator CollectStone()
    {   
        if (StoneTimer == false)
        {   
            StoneTimer = true;
            yield return new WaitForSeconds(WaitSecondsStone);
            Inventory.Stone = Inventory.Stone + increaseStone;
            StoneTimer = false;
        }
    }

    bool IsPlayerCloseToTheObject()
    {
        if (player2.activeSelf)
        {
            // Get the center of the object
            Vector2 objectCenter = new Vector2(this.transform.position.x, this.transform.position.y - this.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            float distance = Vector2.Distance(player2.transform.position, objectCenter);
            return distance <= 0.5f;
        }
        else
        {
            return false;
        }
    }
}

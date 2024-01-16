using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayWalking();
        PlayTreeChopping();
        PlayRecoverySleep();
    }

    void PlayWalking ()
    {
        if (ClickAgentController.isWalking)
        {
            StartCoroutine(PlayAfterWait("Walking", PlayerReaction.popupTimer/2));
            //animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    void PlayTreeChopping()
    {
        if (TreeAnimation.choppingTrees)
        {
            StartCoroutine(PlayForTime("Attacking", TreeAnimation.WaitSeconds));
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    void PlayRecoverySleep()
    {
        if (Exhaustion.playRecoveryAnimation)
        {
            animator.SetBool("Sleeping", true);
        }
        else
        {
            animator.SetBool("Sleeping", false);
        }
    }

    IEnumerator PlayAfterWait(string animName, float waitTime = 0)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool(animName, true);
    }

    IEnumerator StopAfterWait(string animName, float waitTime = 0)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool(animName, false);
    }

    IEnumerator PlayForTime(string animName, float waitTime = 0)
    {
        animator.SetBool(animName, true);
        yield return new WaitForSeconds(waitTime);
        animator.SetBool(animName, false);
    }
}

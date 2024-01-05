using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script plays a reaction animation when the player clicks on any object.
/// </summary>

public class PlayerReactionPlayer : MonoBehaviour
{
    public Animator animator;
    public int animationTimer = 3;
    public static bool react;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator-Komponente fehlt im GameObject");
        }
        else if (animator != null)
        {
            animator.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (react == true && animator != null)
        {
            StartCoroutine(PlayAnimation());
        }
    }

    IEnumerator PlayAnimation()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(animationTimer);
        animator.enabled = false;
        react = false;
    }   
}

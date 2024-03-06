using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowAnimate : MonoBehaviour
{
    GameObject textBox;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        textBox = this.transform.parent.transform.parent.transform.Find("CrowTrigger/TextBox").gameObject;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox.activeSelf == true)
        {
            animator.SetBool("isTalking", true);
        }
        else
        {
            animator.SetBool("isTalking", false);
        }
    }
}

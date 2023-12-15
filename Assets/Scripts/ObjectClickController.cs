using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to every object that can be clicked.
/// It is used to detect clicks on objects.
/// If clicked, the object will play an animation.
/// To recognize clicks, the object needs a 2D collider.
/// </summary>

public class ObjectClickController : MonoBehaviour
{
    public Animation animate;
    public bool isClicked = false;
    public AnimationClip animationName;

    // Start is called before the first frame update
    void Start()
    {
        animate = this.GetComponent<Animation>();
        animate.AddClip(new AnimationClip(), animationName.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (animate.isPlaying == false)
        {
            Debug.Log("Clicked: " + this.name);
            isClicked = true;
            animate.Play(animationName.name);
        }
        else
        {
            Debug.Log("Animation is already playing");
            isClicked = true;
        }
    }
    void OnMouseUp()
    {
        isClicked = false;
    }

}

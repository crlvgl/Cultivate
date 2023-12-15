using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script plays a reaction animation when the player clicks on any object.
/// </summary>

public class PlayerReactionPlayer : MonoBehaviour
{
    public Animation animate;
    public ObjectClickController objectClickController;
    public string animationName = "Reaction";
    private bool playAnimation = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playAnimation = objectClickController.isClicked;
        if (playAnimation && animate.isPlaying == false)
        {
            animate.Play(animationName);
            Debug.Log("Reaction Animation is playing");
        }
    }
}

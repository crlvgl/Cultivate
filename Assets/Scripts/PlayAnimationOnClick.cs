using UnityEngine;
using System.Collections;

public class PlayAnimationOnClick : MonoBehaviour
{
    public Animator animator; // Referenz zum Animator
    public bool Woodtimer = false;
    public float WaitSeconds = 3f;

    void Start()
    {
        // Animator-Komponente erhalten
        animator = GetComponent<Animator>();

        // Sicherstellen, dass eine Animator-Komponente vorhanden ist
        if (animator == null)
        {
            Debug.LogError("Animator-Komponente fehlt im GameObject");
        }
    }

    void OnMouseDown()
    {
        // Start the coroutine to handle the animation
        StartCoroutine(PlayAnimation());
        StartCoroutine(CollectWood());


    }

    IEnumerator PlayAnimation()
    {
        // Check if the Animator is available
        if (animator != null)
        {
            // Enable the Animator
            animator.enabled = true;

            // Wait for 3 seconds
            yield return new WaitForSeconds(WaitSeconds);

            // Disable the Animator
            animator.enabled = false;
        }
    }

    IEnumerator CollectWood()
    {   
        if (Woodtimer == false)
        {   
            Woodtimer = true;
            Inventory.Wood = Inventory.Wood + 1;
            yield return new WaitForSeconds(WaitSeconds);
            Woodtimer = false;
        }
        
    }



    
}
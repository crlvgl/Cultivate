using UnityEngine;
using System.Collections;

public class TreeAnimation : MonoBehaviour
{
    public Animator animator; // Referenz zum Animator
    public bool Woodtimer = false;
    public static float WaitSeconds = 3f;
    public GameObject player1;
    public GameObject player2;

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

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (ClickAgentController.holdStill == false && IsPlayerCloseToTheObject() == true) // to make sure only one tree at a time is klicked
        {
            // Start the coroutine to handle the animation
            StartCoroutine(PlayAnimation());
            StartCoroutine(CollectWood());
        }
    }

    IEnumerator PlayAnimation()
    {
        if (animator != null)
        {
            ClickAgentController.holdStill = true;
            // Enable the Animator
            animator.enabled = true;

            // Wait for 3 seconds
            yield return new WaitForSeconds(WaitSeconds);

            // Disable the Animator
            animator.enabled = false;
            ClickAgentController.holdStill = false;
        }
    }

    IEnumerator CollectWood()
    {   
        if (Woodtimer == false)
        {   
            Woodtimer = true;
            yield return new WaitForSeconds(WaitSeconds);
            Inventory.Wood = Inventory.Wood + 1;
            Woodtimer = false;
        }
    }

    bool IsPlayerCloseToTheObject()
    {
        if (player1.activeSelf)
        {
            // Get the center of the object
            Vector2 objectCenter = new Vector2(this.transform.position.x, this.transform.position.y + this.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            float distance = Vector2.Distance(player1.transform.position, objectCenter);
            return distance <= 0.3f;
        }
        else if (player2.activeSelf)
        {
            // Get the center of the object
            Vector2 objectCenter = new Vector2(this.transform.position.x, this.transform.position.y + this.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            float distance = Vector2.Distance(player2.transform.position, objectCenter);
            return distance <= 0.3f;
        }
        else
        {
            return false;
        }
    }

}

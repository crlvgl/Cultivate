using UnityEngine;
using System.Collections;

public class TreeAnimation : MonoBehaviour
{
    public Animator animator; // Referenz zum Animator
    public bool Woodtimer = false;
    public static float WaitSeconds = 3f;
    public GameObject player1;
    public GameObject player2;
    public static bool chopExhausted = false;
    private TreeProgressBar treeProgressBar;
    private int increaseWood;


    void Start()
    {
        if (DevMode.devMode == true)
        {
            WaitSeconds = 1;
            increaseWood = 5;
        }
        else
        {
            increaseWood = 1;
        }

        // Animator-Komponente erhalten
        animator = GetComponent<Animator>();
        treeProgressBar = GetComponentInChildren<TreeProgressBar>();
        DeactivateProgressBar();

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
        if (chopExhausted == false && Inventory.Relic == 1)
        {
            if (ClickAgentController.holdStill == false && IsPlayerCloseToTheObject() == true) // to make sure only one tree at a time is klicked
            {
                // Start the coroutine to handle the animation
                StartCoroutine(PlayAnimation());
                StartCoroutine(CollectWood());
                StartCoroutine(ProgressBar());
            }
        }
        else if (chopExhausted == true)
        {
            Debug.Log("Exhausted, can't chop anymore");
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

    IEnumerator ProgressBar()
    {
        if (treeProgressBar != null)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            treeProgressBar.Progress(1f);
            yield return new WaitForSeconds(WaitSeconds);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            treeProgressBar.resetProgress();
        }
    }

    public void DeactivateProgressBar()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        treeProgressBar.resetProgress();
    }

    IEnumerator CollectWood()
    {   
        if (Woodtimer == false)
        {   
            Woodtimer = true;
            yield return new WaitForSeconds(WaitSeconds);
            Inventory.Wood = Inventory.Wood + increaseWood;
            Exhaustion.treesChopped += 1;
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

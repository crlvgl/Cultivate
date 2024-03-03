using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrees : MonoBehaviour
{
    public int clicksRequired = 5;
    public GameObject player1;
    public GameObject player2;
    public bool needsActivation = false;
    public int clicks = 0;
    private TreeAnimation treeAnimation;
    private int increaseWood;

    // Start is called before the first frame update
    void Start()
    {
        if (DevMode.devMode == true)
        {
            increaseWood = 5;
        }
        else
        {
            increaseWood = 1;
        }
        
        clicksRequired -= 1;
        treeAnimation = GetComponentInChildren<TreeAnimation>();
    }

    void Update()
        {
            if (clicks >= clicksRequired)
            {
                StartCoroutine(deactivateTree());
            }
        }

    void OnMouseDown()
        {
            if (IsPlayerCloseToTheObject())
            {
                StartCoroutine(increaseClicks());
            }
        }

    IEnumerator increaseClicks()
    {
        yield return new WaitForSeconds(TreeAnimation.WaitSeconds);
        clicks++;
    }

    IEnumerator deactivateTree()
    {
        yield return new WaitForSeconds(TreeAnimation.WaitSeconds);
        needsActivation = true;
        this.gameObject.SetActive(false);
        Inventory.Wood = Inventory.Wood + increaseWood;
        Exhaustion.treesChopped++;
        treeAnimation.DeactivateProgressBar();
        TreeAnimation.choppingTrees = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrees : MonoBehaviour
{
    public int timeToRespawnMin = 5;
    public int timeToRespawnMax = 10;
    private List<GameObject> trees;
    private GameObject treeObject;
    // Start is called before the first frame update
    void Start()
    {
        if (DevMode.devMode == true)
        {
            timeToRespawnMin = 1;
            timeToRespawnMax = 2;
        }
        
        trees = new List<GameObject>();
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree"))
        {
            trees.Add(tree);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject tree in trees)
        {
            if (tree.GetComponent<DestroyTrees>().needsActivation == true)
            {
                treeObject = tree;
                treeObject.GetComponent<DestroyTrees>().needsActivation = false;
                ClickAgentController.holdStill = false;
                StartCoroutine(Respawn());
            }
        }
    }

    IEnumerator Respawn()
    {
        int randomTime = Random.Range(timeToRespawnMin, timeToRespawnMax);
        Debug.Log(treeObject.name);
        Debug.Log("Waiting for " + randomTime + " seconds");
        yield return new WaitForSeconds(randomTime);
        treeObject.GetComponent<DestroyTrees>().clicks = 0;
        treeObject.GetComponent<TreeAnimation>().Woodtimer = false;
        treeObject.GetComponent<TreeAnimation>().animator.enabled = false;
        treeObject.gameObject.SetActive(true);
    }
}

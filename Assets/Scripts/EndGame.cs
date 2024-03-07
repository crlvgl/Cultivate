using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Animator animator;
    public float waitBeforeEndGame1 = 2f;
    public float waitBeforeEndGame2 = 4.5f;
    public float waitBeforeEndGame3 = 3f;
    public static bool disableAll = false;
    public static bool moveToP1 = false;
    public static bool moveToP2 = false;
    public string pathToScene;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator found on " + this.name);
        }
        else
        {
            animator.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.Relic == 2 && disableAll == false)
        {
            disableAll = true;
            StartCoroutine(EndGameAnimation());
        }
    }

    IEnumerator EndGameAnimation()
    {
        moveToP1 = true;
        yield return new WaitForSeconds(waitBeforeEndGame1);
        moveToP1 = false;
        moveToP2 = true;
        yield return new WaitForSeconds(waitBeforeEndGame2);
        animator.enabled = true;
        yield return new WaitForSeconds(6.010f);
        animator.enabled = false;
        yield return new WaitForSeconds(waitBeforeEndGame3);

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(pathToScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

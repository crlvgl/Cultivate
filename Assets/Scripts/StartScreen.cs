using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public string pathToScene;
    [Tooltip("LoadMode: 'new' loads new game, 'load' loads saved game")]
    public string LoadMode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        if (LoadMode == "new")
        {
            StartCoroutine(LoadNewScene());
        }
        else if (LoadMode == "load")
        {
            StartCoroutine(LoadSavedScene());
        }
    }

    IEnumerator LoadNewScene()
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(pathToScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadSavedScene()
    {
        staticInfoClass.loadScene = true;

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(pathToScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

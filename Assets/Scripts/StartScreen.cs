using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreen : MonoBehaviour
{
    [Header("Loading Settings")]
    public string pathToScene;
    [Tooltip("LoadMode: 'new' loads new game, 'load' loads saved game")]
    public string LoadMode;

    SpriteRenderer sprite;
    [Header("Mouse Over Settings")]
    [Header("Sprite Color")]
    [Tooltip("Color shift in RGB format, 0-255")]
    public Vector3 colorShift;
    [Tooltip("Transparency on mouse over; 0-1")]
    public float transparencyOver = 0.75f;
    [Tooltip("Transparency on mouse exit; 0-1")]
    public float transparencyOut = 0.4f;

    [Header("Scale")]
    [Tooltip("if tagged uses percentage of current scale, else uses absolute scale change")]
    public bool percent;
    [Tooltip("Scale change on mouse over in X and Y direction; added to current scale; absolute scale")]
    public Vector2 scaleOver;
    [Tooltip("Scale change on mouse over in X and Y direction; added to current scale; percentage of current scale")]
    public float scaleOverPercent;

    private TMP_Text Text;
    private Color textColor;
    [Header("Text Color")]
    public Color textColorOver = new Color(1f, 1f, 1f, 1f);

    [Header("New Sprite")]
    [Tooltip("if true, only changes sprite, else changes sprite and every other setting")]
    public bool onlyUseSprite;
    [Tooltip("New Sprite to use over the old one during mouse over")]
    public Sprite newSprite;
    private Sprite oldSprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        colorShift = new Vector3(colorShift.x/255, colorShift.y/255, colorShift.z/255);

        Text = this.transform.Find("Canvas/Text").GetComponent<TMP_Text>();
        textColor = Text.color;

        if (percent)
        {
            scaleOver = new Vector2(this.transform.localScale.x*(scaleOverPercent/100), this.transform.localScale.y*(scaleOverPercent/100));
        }

        oldSprite = sprite.sprite;
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

    void OnMouseEnter()
    {
        if (onlyUseSprite == false)
        {
            sprite.color = new Color(sprite.color.r-colorShift.x, sprite.color.g-colorShift.y, sprite.color.b-colorShift.z, transparencyOver);
            this.transform.localScale = new Vector3(this.transform.localScale.x+scaleOver.x, this.transform.localScale.y+scaleOver.y, this.transform.localScale.z);
            Text.color = textColorOver;
        }
        if (newSprite != null)
        {
            sprite.sprite = newSprite;
        }
    }

    void OnMouseExit()
    {
        if (onlyUseSprite == false)
        {
            sprite.color = new Color(sprite.color.r+colorShift.x, sprite.color.g+colorShift.y, sprite.color.b+colorShift.z, transparencyOut);
            this.transform.localScale = new Vector3(this.transform.localScale.x-scaleOver.x, this.transform.localScale.y-scaleOver.y, this.transform.localScale.z);
            Text.color = textColor;
        }
        if (newSprite != null)
        {
            sprite.sprite = oldSprite;
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

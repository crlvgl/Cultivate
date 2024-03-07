using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveExitButton : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Button Settings")]
    [Tooltip("ButtonMode: 'save' saves game, 'exit' saves game and exits")]
    public string ButtonMode;

    SpriteRenderer sprite;
    [Header("Mouse Over Settings")]
    [Header("Sprite Color")]
    [Tooltip("if true, changes color")]
    public bool changeColor;
    [Tooltip("Color shift in RGB format, 0-255")]
    public Vector3 colorShift;
    [Tooltip("Transparency on mouse over; 0-1")]
    public float transparencyOver = 0.75f;
    [Tooltip("Transparency on mouse exit; 0-1")]
    public float transparencyOut = 0.4f;

    [Header("Scale")]
    [Tooltip("if true, changes scale")]
    public bool changeScale;
    [Tooltip("if tagged uses percentage of current scale, else uses absolute scale change")]
    public bool percent;
    [Tooltip("Scale change on mouse over in X and Y direction; added to current scale; absolute scale")]
    public Vector2 scaleOver;
    [Tooltip("Scale change on mouse over in X and Y direction; added to current scale; percentage of current scale")]
    public float scaleOverPercent;

    private TMP_Text Text;
    private Color textColor;
    [Header("Text Color")]
    [Tooltip("if true, changes text color")]
    public bool changeTextColor;
    [Tooltip("Text color on mouse over")]
    public Color textColorOver = new Color(1f, 1f, 1f, 1f);

    [Header("New Sprite")]
    [Tooltip("if true, changes sprite")]
    public bool UseSprite;
    [Tooltip("New Sprite to use over the old one during mouse over")]
    public Sprite newSprite;
    private Sprite oldSprite;

    public static bool saveGame = false;

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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        saveGame = true;
        if (ButtonMode == "exit")
        {
            Application.Quit();
            Debug.Log("Quit game");
            Debug.Log("needs to be testet in compiled game");
        }
    }

    void OnMouseEnter()
    {
        if (changeColor)
        {
            sprite.color = new Color(sprite.color.r-colorShift.x, sprite.color.g-colorShift.y, sprite.color.b-colorShift.z, transparencyOver);
        }
        if (changeScale)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x+scaleOver.x, this.transform.localScale.y+scaleOver.y, this.transform.localScale.z);
        }
        if (changeTextColor)
        {
            Text.color = textColorOver;
        }
        if (newSprite != null && UseSprite == true)
        {
            sprite.sprite = newSprite;
        }
        Debug.Log(this.name);
        audioSource.Play();
    }

    void OnMouseExit()
    {
        if (changeColor)
        {
            sprite.color = new Color(sprite.color.r+colorShift.x, sprite.color.g+colorShift.y, sprite.color.b+colorShift.z, transparencyOut);
        }
        if (changeScale)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x-scaleOver.x, this.transform.localScale.y-scaleOver.y, this.transform.localScale.z);
        }
        if (changeTextColor)
        {
            Text.color = textColor;
        }
        if (newSprite != null && UseSprite == true)
        {
            sprite.sprite = oldSprite;
        }
    }
}

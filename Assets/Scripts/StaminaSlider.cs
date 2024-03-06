using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSlider : MonoBehaviour
{
    private Slider slider;
    private GameObject fillGameObject;

    // Start is called before the first frame update
    void Start()
    {
// Get the Slider component
        slider = GetComponent<Slider>();

        // Find the "Fill" child GameObject
        fillGameObject = transform.Find("Fill Area").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Exhaustion.exhaustionPoints/100;
    }
}

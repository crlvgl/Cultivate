using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneProgressBar : MonoBehaviour
{
    private Slider slider;
    private float targetProgress = 0;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void Progress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

    void Update()
    {
        if (slider.value < targetProgress)
            slider.value += Time.deltaTime / Stone.WaitSecondsStone;
    }

    public void resetProgress()
    {
        slider.value = 0;
    }

}

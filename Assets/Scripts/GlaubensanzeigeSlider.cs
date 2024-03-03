using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlaubensanzeigeSlider : MonoBehaviour
{
    public float GlaubensanzeigeTargetValue = 0f;
    private Slider slider;
    private GameObject fillGameObject;
    private float FillSpeed = 0.1f;
    private ParticleSystem ParticleSys;

    // Start is called before the first frame update
    void Start()
    {
// Get the Slider component
        slider = GetComponent<Slider>();
        ParticleSys = GameObject.Find("Fill/SliderParticles").GetComponent<ParticleSystem>();

        // Find the "Fill" child GameObject
        fillGameObject = transform.Find("Fill Area").gameObject;

        // Deactivate the "Fill" child GameObject
        if (fillGameObject != null)
        {
            fillGameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GlaubensanzeigeTargetValue > 0)
        {
            fillGameObject.SetActive(true);
        }
        if (GlaubensanzeigeTargetValue > slider.value)
        {
            slider.value += Time.deltaTime * FillSpeed;
            if (!ParticleSys.isPlaying)
                ParticleSys.Play();
        }
        else
        {
            ParticleSys.Stop();
        }

        if(Inventory.Relic == 1)
        {
            GlaubensanzeigeTargetValue = 0.5f;
        } 
        if(Inventory.Altar == 1)
        {
            GlaubensanzeigeTargetValue = 1f;
        } 
    }
}

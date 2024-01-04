using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the parallax effect of GameObjects such as Clouds.
/// For a decent Effect, a parallaxEffect of 0.05 is recommended.
/// </summary>

public class ParallaxEffect : MonoBehaviour
{
    private float startPos;
    public float parallaxEffect;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = cam.transform.position;
        float temp = camPos.x * (1 - parallaxEffect);
        float dist = camPos.x * parallaxEffect;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}

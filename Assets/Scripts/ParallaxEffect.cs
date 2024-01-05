using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the parallax effect of GameObjects such as Clouds.
/// For a decent Effect, a parallaxEffect of 0.05 is recommended.
/// </summary>

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxEffectX;
    public float parallaxEffectY;
    public Camera cam;
    private float startPosX;
    private float startPosY;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = cam.transform.position;
        float distX = camPos.x * parallaxEffectX;
        float distY = camPos.y * parallaxEffectY;

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
    }
}

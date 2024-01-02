using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera gameCamera;
    private float zoomSpeed = 2f; // Speed of zooming
    private bool isZooming = false;
    public Vector3 zoomScale = new Vector3(3f, 3f, 1f);
    private Vector3 originalScale;
    private float zoomTimer = 0f;
    private float zoomDuration = 3f;


    public static Vector3 CameraPosition;
    //private Vector3 Offset = new Vector3(0f,0f,-10f);
    private Vector3 Velocity = Vector3.zero;

    [Header("Camera Settings")]
    public float CameraSpeed = 2f;
    public bool WASD = true;
    public float SmoothTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        CameraPosition = this.transform.position;    
        originalScale = gameCamera.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (WASD == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                CameraPosition.y += CameraSpeed / 1000;
            }
            if (Input.GetKey(KeyCode.S))
            {
                CameraPosition.y -= CameraSpeed / 1000;
            }
            if (Input.GetKey(KeyCode.A))
            {
                CameraPosition.x -= CameraSpeed / 1000;
            }
            if (Input.GetKey(KeyCode.D))
            {
                CameraPosition.x += CameraSpeed / 1000;
            }

            this.transform.position = CameraPosition;
        }
        else
        {
            if (ClickAgentController.holdStill == false)
            {
                if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
                {
                    // Store the clicked position
                    CameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    CameraPosition.z = transform.position.z; // Keep the current z position
                }

                // Move towards the stored position smoothly
                transform.position = Vector3.SmoothDamp(transform.position, CameraPosition, ref Velocity, SmoothTime);
            }
            
        }
        if (isZooming) //zoom in
        {
            if (zoomTimer < zoomDuration)
            {
                // Smoothly interpolate towards the zoomScale
                gameCamera.transform.localScale = Vector3.Lerp(gameCamera.transform.localScale, zoomScale, Time.deltaTime * zoomSpeed);
                Debug.Log(Time.deltaTime);
                zoomTimer += Time.deltaTime;
            }
            else
            {
                // Directly set to the zoomScale after duration
                gameCamera.transform.localScale = zoomScale;
            }
        }
        else //zoom out
        {
            if (zoomTimer < zoomDuration)
            {
                // Smoothly interpolate back to the original scale
                gameCamera.transform.localScale = Vector3.Lerp(gameCamera.transform.localScale, originalScale, zoomSpeed * Time.deltaTime);
                zoomTimer += Time.deltaTime;
            }
            else
            {
                // Directly set to the original scale after duration
                gameCamera.transform.localScale = originalScale;
            }
        }
        Debug.Log(zoomTimer);
    }

    public void StartZooming()
    {
        isZooming = true;
        zoomTimer = 0f; // Reset the timer
    }

    public void StopZooming()
    {
        isZooming = false;
        zoomTimer = 0f; // Reset the timer
    }
}

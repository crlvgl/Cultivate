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
    private bool lockOnPlayer = false;


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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (WASD == true)
            {
                WASD = false;
            }
            else
            {
                WASD = true;
                lockOnPlayer = false;
            }
        }
        if (WASD == true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                WASD = false;
                lockOnPlayer = true;
                CameraPosition = GetPlayerPosition();
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        CameraPosition.y += CameraSpeed / 500;
                    }
                    else
                    {
                        CameraPosition.y += CameraSpeed / 1000;
                    }
                }
                if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        CameraPosition.y -= CameraSpeed / 500;
                    }
                    else
                    {
                        CameraPosition.y -= CameraSpeed / 1000;
                    }
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        CameraPosition.x -= CameraSpeed / 500;
                    }
                    else
                    {
                        CameraPosition.x -= CameraSpeed / 1000;
                    }
                }
                if (Input.GetKey(KeyCode.D))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        CameraPosition.x += CameraSpeed / 500;
                    }
                    else
                    {
                        CameraPosition.x += CameraSpeed / 1000;
                    }
                }

                this.transform.position = CameraPosition;
            }
        }
        else if (WASD == false && lockOnPlayer == false)
        {
            if (ClickAgentController.holdStill == false)
            {
                if (Input.GetMouseButtonDown(1)) // 0 is the left mouse button
                {
                    // Store the clicked position
                    CameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    CameraPosition.z = transform.position.z; // Keep the current z position
                }

                // Move towards the stored position smoothly
                transform.position = Vector3.SmoothDamp(transform.position, CameraPosition, ref Velocity, SmoothTime);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                lockOnPlayer = true;
            }
        }
        else if (WASD == false && lockOnPlayer == true)
        {
            if (ClickAgentController.holdStill == false)
            {
                transform.position = GetPlayerPosition();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                lockOnPlayer = false;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                WASD = true;
                lockOnPlayer = false;
            }
        }
        if (isZooming) //zoom in
        {
            if (zoomTimer < zoomDuration)
            {
                // Smoothly interpolate towards the zoomScale
                gameCamera.transform.localScale = Vector3.Lerp(gameCamera.transform.localScale, zoomScale, Time.deltaTime * zoomSpeed);
                //Debug.Log(Time.deltaTime);
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
        //Debug.Log(zoomTimer);
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

    Vector3 GetPlayerPosition()
    {
        Vector3 safePosition = new Vector3(0f, 0f, 0f);
        safePosition.x = GameObject.FindWithTag("Player").transform.position.x;
        safePosition.y = GameObject.FindWithTag("Player").transform.position.y;
        safePosition.z = this.transform.position.z;
        return safePosition;
    }
}

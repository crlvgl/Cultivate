using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 CameraPosition;
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
}

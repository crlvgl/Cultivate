using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAISpots : MonoBehaviour
{
    private float CameraX = 5f;
    private float CameraY = 2.5f;
    private Vector2 centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        CalculateCenterPoint();
        UpdateCircleColors();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCenterPoint();
        UpdateCircleColors();
    }

    void CalculateCenterPoint()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            centerPoint = cam.transform.position;
        }
    }

    bool IsWithinBounds(Vector3 position)
    {
        return position.x >= (centerPoint.x - CameraX) && position.x <= (centerPoint.x + CameraX) &&
            position.y >= (centerPoint.y - CameraY) && position.y <= (centerPoint.y + CameraY);
    }

    void UpdateCircleColors()
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if (IsWithinBounds(child.position))
                {
                    spriteRenderer.color = Color.white;
                }
                else
                {
                    spriteRenderer.color = Color.red;
                }
            }
        }
    }
}

using UnityEngine;

public class ZoomZone : MonoBehaviour
{
    public CameraMovement CameraMovement;
    private bool Zooming = false;
    public Transform playerTransform;
   // public static Vector2 Zoomlocation;


    void Update()
    {
        if (IsInTriggerZone() == true && Zooming == false)
        {
       //     Zoomlocation = transform.position;
            CameraMovement.StartZooming();
            Zooming = true;
            Debug.Log("Zooming in");
        }
        if (IsInTriggerZone() == false && Zooming == true)
        {
            CameraMovement.StopZooming();
            Zooming = false;
            Debug.Log("Zooming out");
        }

    }


    bool IsInTriggerZone()
    {
        
        float distance = Vector2.Distance(playerTransform.position, this.transform.position);
        //Debug.Log(distance);
        return distance <= 2f;

    }
}

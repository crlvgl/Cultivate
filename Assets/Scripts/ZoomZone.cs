using UnityEngine;

public class ZoomZone : MonoBehaviour
{
    public CameraMovement CameraMovement;
    private bool Zooming = false;
    public GameObject player1;
    public GameObject player2;
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
        if(gameObject.name == "ZoomZone_Forest") //special treatment
        {
            if (player1.activeSelf)
            {
                float distance = Vector2.Distance(player1.transform.position, new Vector2(4,16));
                //Debug.Log(distance);
                return distance <= 4f;
            }
            else if (player2.activeSelf)
            {
                float distance = Vector2.Distance(player2.transform.position, this.transform.position);
                //Debug.Log(distance);
                return distance <= 4f;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (player1.activeSelf)
            {
                float distance = Vector2.Distance(player1.transform.position, this.transform.position);
                //Debug.Log(distance);
                return distance <= 2f;
            }
            else if (player2.activeSelf)
            {
                float distance = Vector2.Distance(player2.transform.position, this.transform.position);
                //Debug.Log(distance);
                return distance <= 2f;
            }
            else
            {
                return false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script controls the characters movement.
/// It is used to move the character to the position of the mouse click.
/// To use this script, the script needs to be attached to the character.
/// The script also needs a NavMeshAgent component and Navigation Modifier.
/// </summary>

public class ClickAgentController : MonoBehaviour
{
    private Vector2 target;
    private Vector2 playerPosition;
    private Vector2 direction;
    private float walkingDistance;
    private IEnumerator moveToRandomPositionCoroutine;
    private int PlayerControlCache;
    private Vector2 centerPoint;
    private List<Vector3> tempcoordinates;
    public bool pressedButton = true;
    private Coroutine timerCoroutine;

    private List<Vector3> allCoordinates;
    private Vector3 lastPosition;
    private string developerInfo = ""; // Information to display on screen
    private string developerInfo2 = "";

    [Header("Player Control")]
    public int PlayerControl = 4;
    public float walkingDistance1 = 20.0f;
    public float walkingDistance2 = 30.0f;
    public float walkingDistance3 = 40.0f;

    [Header("Player AI Settings")]
    public float CameraX = 5f;
    public float CameraY = 2.5f;
    public bool PlayerAi = false;
    public bool developerMode = true;
    public int minWaitTime = 3;
    public int maxWaitTime = 5;
    

    // import agent component
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        // import agent component
        agent = GetComponent<NavMeshAgent>();
        // prevent Unity from turning agent out of 2d plane
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Besondere Punkte auf der Map zu denen der Spieler laufen soll
        allCoordinates = new List<Vector3>();
        allCoordinates.Add(new Vector3(-2, 2, 0));
        allCoordinates.Add(new Vector3(1, 1, 0));
        allCoordinates.Add(new Vector3(2, 1, 0));
        allCoordinates.Add(new Vector3(1, -1, 0));

        CalculateCenterPoint();
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        FindTarget();
        MoveAgent();
        ManageMovementCoroutine();

        // Check if any key or mouse button is pressed
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            // Start or restart the timer when a button is pressed
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
            timerCoroutine = StartCoroutine(ButtonPressedTimer());
        }
    }

    void FixedUpdate()
    {
        
    }

    void SetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("original target: " + target);
            //Debug.Log("position: " + transform.position);
            //Debug.Log("true distance: " + Vector2.Distance(transform.position, target));
        }
    }

    void MoveAgent()
    {
        // Debug.Log("target: " + target + " position: " + transform.position);
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }

    void FindTarget()
    {
        if (PlayerControl == 1)
        {
            walkingDistance = walkingDistance1;
        }

        else if (PlayerControl == 2)
        {
            walkingDistance = walkingDistance2;
        }

        else if (PlayerControl == 3)
        {
            walkingDistance = walkingDistance3;
        }

        else if (PlayerControl == 4)
        {
            walkingDistance = Mathf.Infinity;
        }

        if (Vector2.Distance(transform.position, target) > walkingDistance)
            {
                playerPosition = new Vector2(transform.position.x, transform.position.y);
                direction = target - playerPosition;
                direction.Normalize();
                target = playerPosition + (direction * walkingDistance);
                //Debug.Log("new target: " + target);
                //Debug.Log("disstance: " + Vector2.Distance(transform.position, target));
            }
    }

    // Player zwischen den Koordinaten der Liste allCoordinates hin und her laufen lassen
    IEnumerator MoveToRandomPosition()
    {
        Vector3 newPosition = transform.position;
        while (PlayerAi)
        {   
            CalculateCenterPoint();
            tempcoordinates = new List<Vector3>(allCoordinates);
            List<Vector3> coordinatesToRemove = new List<Vector3>();
            // Find coordinates that are out of bounds
            foreach (Vector3 coordinate in tempcoordinates)
            {
                if (!IsWithinBounds(coordinate))
                {
                    coordinatesToRemove.Add(coordinate);
                }
            }

            // Remove the out-of-bounds coordinates
            foreach (Vector3 coordinate in coordinatesToRemove)
            {
                tempcoordinates.Remove(coordinate);
            }

            newPosition = GetRandomPosition();

            int waitTime = Random.Range(minWaitTime, maxWaitTime);
            if (developerMode)
            {
                developerInfo = "Moving to: " + newPosition + "\nStay duration: " + waitTime + " seconds\nlastPosition: " + lastPosition + "\ncenterPoint: " + centerPoint + "\nallCoordinates.Count: " + allCoordinates.Count + "\ntempcoordinates.Count: " + tempcoordinates.Count + "\ncoordinatesToRemove.Count: " + coordinatesToRemove.Count;
            }

            // Move to the new position
            
            target = newPosition;
            MoveAgent();
            // Wait for the random time
            yield return new WaitForSeconds(waitTime);

            if (developerMode)
            {
                
            }
            developerInfo = ""; // Clear the info after moving
        }
    }

    //Checkt ob PlayerAi an oder aus ist (ist wichtig zum automatischen an und aus schalten)
    void ManageMovementCoroutine()
    {
        if (PlayerAi && moveToRandomPositionCoroutine == null)
        {
            // Start the coroutine if PlayerAi is true and the coroutine isn't already running
            PlayerControlCache = PlayerControl;
            PlayerControl = 4;
            moveToRandomPositionCoroutine = MoveToRandomPosition();
            StartCoroutine(moveToRandomPositionCoroutine);
        }
        else if (!PlayerAi && moveToRandomPositionCoroutine != null)
        {
            // Stop the coroutine if PlayerAi is false and the coroutine is running
            StopCoroutine(moveToRandomPositionCoroutine);
            PlayerControl = PlayerControlCache;
            moveToRandomPositionCoroutine = null;
        }
    }
    
    // Anzeige developerMode
    void OnGUI()
    {
        if (developerMode)
        {
            GUI.Label(new Rect(10, 10, 300, 200), developerInfo);
            GUI.Label(new Rect(900, 10, 300, 200), developerInfo2);
        }
    }

    // Random Position aus der Liste allCoordinates suchen
    Vector3 GetRandomPosition()
    {
        Vector3 newPosition;
        do
        {
            newPosition = tempcoordinates[Random.Range(0, tempcoordinates.Count)];
        }
        while (newPosition == lastPosition);
        lastPosition = newPosition;
        return newPosition;
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

    IEnumerator ButtonPressedTimer()
    {
        // Set pressedButton to true and wait for 60 seconds
        pressedButton = true;
        PlayerAi = false;
        if (developerMode)
            {
                developerInfo2 ="pressedButton = " + pressedButton;
            }
        yield return new WaitForSeconds(5);

        // After 60 seconds, set pressedButton to false
        pressedButton = false;
        PlayerAi = true;
        if (developerMode)
            {
                developerInfo2 ="pressedButton = " + pressedButton;
            }
    }

}



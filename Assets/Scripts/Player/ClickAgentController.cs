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
    public static Vector2 target;
    private Vector2 playerPosition;
    private Vector2 direction;
    private float walkingDistance;
    private bool startedWalking = false;
    private float walkingTimer;
    public static bool isWalking = false;
    private IEnumerator moveToRandomPositionCoroutine;
    private int PlayerControlCache;
    private Vector2 centerPoint;
    private List<Vector2> tempcoordinates;
    public bool pressedButton = true;
    private Coroutine timerCoroutine;

    private List<Vector2> allCoordinates;
    private Vector3 lastPosition;
    private string developerInfo = ""; // Information to display on screen
    private string developerInfo2 = "";
    private int chance;

    public static bool holdStill = false;

    [Header("Player Control")]
    public int PlayerControlSetting = 1;
    public static int PlayerControl;
    public float walkingDistance1Setting = 1.0f;
    public static float walkingDistance1;
    public float walkingDistance2Setting = 3.0f;
    public static float walkingDistance2;
    public float walkingDistance3Setting = 5.0f;
    public static float walkingDistance3;

    [Header("Player AI Settings")]
    public bool PlayerAiOff = false;
    public float CameraX = 5f;
    public float CameraY = 2.5f;
    public static bool PlayerAi = false;
    public bool developerMode = true;
    public int minWaitTime = 10;
    public int maxWaitTime = 15;
    

    // import agent component
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControl = PlayerControlSetting;
        walkingDistance1 = walkingDistance1Setting;
        walkingDistance2 = walkingDistance2Setting;
        walkingDistance3 = walkingDistance3Setting;
        // import agent component
        agent = GetComponent<NavMeshAgent>();
        // prevent Unity from turning agent out of 2d plane
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Besondere Punkte auf der Map zu denen der Spieler laufen soll
        allCoordinates = new List<Vector2>();
        allCoordinates.Add(new Vector2(2, 5));
        allCoordinates.Add(new Vector2(7, 6));
        allCoordinates.Add(new Vector2(3, 7));
        allCoordinates.Add(new Vector2(3, 6));

        CalculateCenterPoint();

        target = new Vector2(this.transform.position.x, this.transform.position.y);
        MoveAgent();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (holdStill == false)
        {
            SetTargetPosition();
            FindTarget();
            MoveAgent();
            getWalkingTime();
            ManageMovementCoroutine();
        }

        ManageAnimation();

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

        if(Inventory.Relic == 1)
        {
            PlayerControl = 2;
            holdStill = true;
            holdStill = false;
        }
        if(Inventory.Altar == 1)
        {
            PlayerControl = 3;
            holdStill = true;
            holdStill = false;
        }

    }

    void FixedUpdate()
    {
        
    }

    void SetTargetPosition()
    {
        if (Input.GetMouseButtonDown(1))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            chance = Random.Range(0, 100);
            //Debug.Log("original target: " + target);
            //Debug.Log("position: " + transform.position);
            //Debug.Log("true distance: " + Vector2.Distance(transform.position, target));
        }
    }

    void MoveAgent()
    {
        if (target != null && target != new Vector2(this.transform.position.x, this.transform.position.y))
        {
            StartCoroutine(WaitWhilePopupBeforeWalking());
        }
        else
        {
            // Debug.Log("target: " + target + " position: " + transform.position);
            agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
        }
    }

    IEnumerator WaitWhilePopupBeforeWalking()
    {
        // Debug.Log(PlayerReaction.popupTimer);
        yield return new WaitForSeconds((PlayerReaction.popupTimer/2));
        // Debug.Log("target: " + target + " position: " + transform.position);
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }

    void getWalkingTime()
    {
        if (target != null && target != new Vector2(this.transform.position.x, this.transform.position.y) && startedWalking == false)
        {
            walkingTimer = Time.time;
            startedWalking = true;
            // Debug.Log("started Timer");
        }
        else if (target != null && target != new Vector2(this.transform.position.x, this.transform.position.y) && startedWalking == true)
        {
            Exhaustion.distanceWalked = 2 * (Time.time - walkingTimer);
            // Debug.Log("Updated Timer " + Exhaustion.distanceWalked);
            if (Exhaustion.distanceWalked >= Exhaustion.exhaustionTimer)
            {
                walkingTimer = Time.time;
                // Debug.Log("Reset Timer");
            }
        }
        else if (target != null && target == new Vector2(this.transform.position.x, this.transform.position.y) && startedWalking == true)
        {
            startedWalking = false;
            Exhaustion.distanceWalked =2 * (Time.time - walkingTimer);
            // Debug.Log("Stopped Timer " + Exhaustion.distanceWalked);
        }
    }

    void ManageAnimation()
    {
        if (target != null && target != new Vector2(this.transform.position.x, this.transform.position.y))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    void FindTarget()
    {
        if (PlayerControl == 1)
        {
            Debug.Log("PlayerControl == 1");
            if (chance < 40)
            {
                Debug.Log("chance < 30 --> " + chance);
                walkingDistance = walkingDistance1;
            }
            else
            {
                walkingDistance = 0f;
            }
        }

        else if (PlayerControl == 2)
        {
            Debug.Log("PlayerControl == 2");
            if (chance < 50)
            {
                walkingDistance = walkingDistance2;
            }
            else
            {
                walkingDistance = 0f;
            }
        }

        else if (PlayerControl == 3)
        {
            Debug.Log("PlayerControl == 3");
            if (chance < 80)
            {
                walkingDistance = walkingDistance3;
            }
            else
            {
                walkingDistance = 0f;
            }
        }

        else if (PlayerControl == 4)
        {
            Debug.Log("PlayerControl == 4");
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
            tempcoordinates = new List<Vector2>(allCoordinates);
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
        if (PlayerAiOff == false){
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

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickAgentController : MonoBehaviour
{
    private Vector2 target;
    private Ray2D ray;
    private float walkingDistance;
    private IEnumerator moveToRandomPositionCoroutine;
    private int PlayerControlCache;
    private Vector2 centerPoint;

    private List<Vector3> coordinates;
    private Vector3 lastPosition;
    private string developerInfo = ""; // Information to display on screen

    [Header("Player Control")]
    public int PlayerControl = 4;
    public float walkingDistance1 = 20.0f;
    public float walkingDistance2 = 30.0f;
    public float walkingDistance3 = 40.0f;

    [Header("Player AI Settings")]
    public float CameraX = 5f;
    public float CameraY = 2.5f;
    public bool PlayerAi = true;
    public bool developerMode = true;
    

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
        coordinates = new List<Vector3>();
        coordinates.Add(new Vector3(-2, 2, 0));
        coordinates.Add(new Vector3(1, 1, 0));
        coordinates.Add(new Vector3(2, 1, 0));
        coordinates.Add(new Vector3(1, -1, 0));

        CalculateCenterPoint();
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        FindTarget();
        MoveAgent();
        ManageMovementCoroutine();
    }

    void FixedUpdate()
    {
        
    }

    void SetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("original target: " + target);
            Debug.Log("position: " + transform.position);
            Debug.Log("true distance: " + Vector2.Distance(transform.position, target));
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
                ray = new Ray2D(transform.position, target);
                target = ray.GetPoint(walkingDistance);
                Debug.Log("new target: " + target);
                Debug.Log("ray: " + ray);
                Debug.Log("disstance: " + Vector2.Distance(transform.position, target));
            }
    }

    // Player zwischen den Koordinaten der Liste coordinates hin und her laufen lassen
    IEnumerator MoveToRandomPosition()
    {
        int attempts = 0;
        const int maxAttempts = 10;
        Vector3 newPosition = transform.position;
        while (PlayerAi)
        {   
            CalculateCenterPoint();
            do
            {
                newPosition = GetRandomPosition();
                attempts++;
                if (attempts >= maxAttempts)
                {
                    Debug.LogWarning("Max attempts reached.");
                    attempts = 0;
                    break;
                }
            }
            while(IsWithinBounds(newPosition) == false);

            int waitTime = Random.Range(3, 5);

            if (developerMode)
            {
                developerInfo = "Moving to: " + newPosition + "\nStay duration: " + waitTime + " seconds\nlastPosition: " + lastPosition + "\nattempts: " + attempts + "\ncenterPoint: " + centerPoint;
            }
            attempts = 0;
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
        }
    }

    // Random Position aus der Liste coordinates suchen
    Vector3 GetRandomPosition()
    {
        Vector3 newPosition;
        do
        {
            newPosition = coordinates[Random.Range(0, coordinates.Count)];
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

}



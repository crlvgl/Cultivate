using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickAgentController : MonoBehaviour
{
    private Vector2 target;
    private Ray2D ray;
    private float walkingDistance;

    [Header("Player Control")]
    public int PlayerControl = 4;
    public float walkingDistance1 = 20.0f;
    public float walkingDistance2 = 30.0f;
    public float walkingDistance3 = 40.0f;

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
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        FindTarget();
        MoveAgent();
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
}

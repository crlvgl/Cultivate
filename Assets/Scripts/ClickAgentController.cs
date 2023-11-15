using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickAgentController : MonoBehaviour
{
    private Vector3 target;

    [Header("Camera Settings")]
    public int PlayerControl = 4;
    public float PlayerControl1Distance = 1f;

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
        MoveAgent();
    }

    void SetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerControl == 1)
            {
                Vector3 direction = (Input.mousePosition - this.transform.position).normalized;
                target = this.transform.position + direction * PlayerControl1Distance; 
            }

            if (PlayerControl == 4)
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    void MoveAgent()
    {
        if (PlayerControl == 0)
        {
            
        }

        if (PlayerControl == 1)
        {
            agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));   
        }

        if (PlayerControl == 2)
        {
            
        }

        if (PlayerControl == 3)
        {
            
        }

        if (PlayerControl == 4)
        {
            agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
        }
        
    }
}

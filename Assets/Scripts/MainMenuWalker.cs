using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWalker : MonoBehaviour
{
    public bool Player;
    public float minTime = 5;
    public float maxTime = 20;
    public Animator animator;
    private Vector2 target;
    UnityEngine.AI.NavMeshAgent agent;
    private bool targetNotChosen = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = new Vector2(this.transform.position.x, this.transform.position.y);
        MoveAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance == 0 && targetNotChosen)
        {
            targetNotChosen = false;
            StartCoroutine(SetTargetPosition());
        }
        MoveAgent();
        Animate();
    }

    void MoveAgent()
    {
        agent.SetDestination(new Vector3(target.x, target.y, this.transform.position.z));
    }

    IEnumerator SetTargetPosition()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        target = new Vector2(Random.Range(-9.1f, 9.1f), Random.Range(-4.9f, 4.9f));
        yield return new WaitForSeconds(1);
        targetNotChosen = true;
    }
    void Animate()
    {
        if (Player)
        {
            if (agent.velocity.magnitude > 0)
            {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
    }
}

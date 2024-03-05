using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimey : MonoBehaviour
{
    public int waitTimeMin = 5;
    public int waitTimeMax = 20;
    public float radiusAroundHouse = 5f;
    public float radiusAroundPlayer = 0.5f;
    public float pettingTime = 3f;
    private Vector2 target;
    private bool isMoving = false;
    private bool isPetting = false;
    private bool mouseOnSlime = false;
    public GameObject player_0;
    public GameObject player_1;
    public Animator animator;
    
    UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = new Vector2(this.transform.position.x, this.transform.position.y);
        MoveAgent();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player_0.transform.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player_1.transform.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        DisableCollision();
        SetTargetPosition();
        MoveAgent();
    }

    void OnMouseDown()
    {
        Debug.Log(IsAroundPlayer());
        if (isPetting == false)
        {
            if (IsAroundPlayer() == true && isMoving == false)
                {
                    StartCoroutine(RestoreExhaustion());
                }
        }
    }

    void OnMouseOver()
    {
        if (player_1.activeSelf == true)
        {
            if (Vector2.Distance(GameObject.Find("PlayerHome").transform.position, player_1.transform.position) <= radiusAroundHouse)
            {
                mouseOnSlime = true;
            }
            else
            {
                mouseOnSlime = false;
            }
        }
        else if (player_0.activeSelf == true)
        {
            if (Vector2.Distance(GameObject.Find("PlayerHome").transform.position, player_0.transform.position) <= radiusAroundHouse)
            {
                mouseOnSlime = true;
            }
            else
            {
                mouseOnSlime = false;
            }
        }
        else
        {
            mouseOnSlime = false;
        }
    }

    void onMouseExit()
    {
        mouseOnSlime = false;
    }

    void MoveAgent()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }

    void SetTargetPosition()
    {
        if (isMoving == false && IsAroundPlayer() == false && mouseOnSlime == false && PlayerWithinRange() == false)
        {
            StartCoroutine(ChangeTargetPosition());
        }
        else if (isMoving == false && IsAroundPlayer() == true && mouseOnSlime == false && PlayerWithinRange() == true)
        {
            if (player_0.activeSelf == true)
            {
                if (Vector2.Distance(this.transform.position, GameObject.Find("PlayerHome").transform.position) <= radiusAroundHouse)
                {
                    target = (Vector2)player_0.transform.position + new Vector2(-0.1f, 0.1f);
                }
            }
            else if (player_1.activeSelf == true)
            {
                if (Vector2.Distance(this.transform.position, GameObject.Find("PlayerHome").transform.position) <= radiusAroundHouse)
                {
                    target = (Vector2)player_1.transform.position + new Vector2(-0.1f, 0.1f);
                }
            }
        }
        else if (isMoving == false && IsAroundPlayer() == true && mouseOnSlime == false && PlayerWithinRange() == false)
        {
            StartCoroutine(ChangeTargetPosition());
        }
        else if (isMoving == false && IsAroundPlayer() == false && mouseOnSlime == false && PlayerWithinRange() == true)
        {
            StartCoroutine(ChangeTargetPosition());
        }
    }

    IEnumerator ChangeTargetPosition()
    {
        isMoving = true;
        yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));
        target = (Vector2)GameObject.Find("PlayerHome").transform.position + (Random.insideUnitCircle * radiusAroundHouse);
        isMoving = false;
    }

    IEnumerator RestoreExhaustion()
    {
        isPetting = true;
        isMoving = true;
        ClickAgentController.holdStill = true;
        animator.SetBool("Happy", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Happy", false);
        yield return new WaitForSeconds(pettingTime-0.2f);
        if (100-Exhaustion.recoverViaPetting < Exhaustion.exhaustionPoints && Exhaustion.exhaustionPoints < 100)
        {
            Exhaustion.exhaustionPoints = 100;
        }
        else if (Exhaustion.exhaustionPoints < 100-Exhaustion.recoverViaPetting)
        {
            Exhaustion.exhaustionPoints += Exhaustion.recoverViaPetting;
        }
        animator.SetBool("Happy", false);
        ClickAgentController.holdStill = false;
        isPetting = false;
        isMoving = false;
    }

    bool IsAroundPlayer()
    {
        if (player_0.activeSelf == true)
        {
            if (Vector2.Distance(this.transform.position, player_0.transform.position) <= radiusAroundPlayer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (player_1.activeSelf == true)
        {
            if (Vector2.Distance(this.transform.position, player_1.transform.position) <= radiusAroundPlayer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    bool PlayerWithinRange()
    {
        if (player_0.activeSelf == true)
        {
            return Vector2.Distance(GameObject.Find("PlayerHome").transform.position, player_0.transform.position) <= radiusAroundHouse;
        }
        else if (player_1.activeSelf == true)
        {
            return Vector2.Distance(GameObject.Find("PlayerHome").transform.position, player_1.transform.position) <= radiusAroundHouse;
        }
        else
        {
            return false;
        }
    }

    bool IsMoving()
    {
        if (agent.velocity.magnitude > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DisableCollision()
    {
        if (DevMode.activateSprintV2 == true)
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), player_1.transform.GetComponent<Collider2D>());
            DevMode.activateSprintV2 = false;
        }
    }
}

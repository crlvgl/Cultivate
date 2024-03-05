using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaustion : MonoBehaviour
{
    public static float exhaustionPoints = 100;
    public static float distanceWalked = 0;
    public static float exhaustionTimer;
    public static int treesChopped = 0;
    public static int flowersPicked = 0;
    public static bool playRecoveryAnimation = false;

    [Header("Exhauster")]
    [Tooltip("How far the player can walk before exhaustion, in Seconds")]
    public int distanceToExhaustion = 5;
    public int treesToExhaustion = 5;
    public int flowersToExhaustion = 15;

    [Header("Player Settings")]
    public GameObject player_0;
    public GameObject player_1;
    private GameObject player;
    public GameObject playerHome;

    [Header("Exhaustion Settings")]
    [Tooltip("when exhaustion effects will stop to take effect when recovering")]
    public int recovery1 = 30;
    [Tooltip("when exhaustion effects will start to take effect")]
    public int exhaustion1 = 50;
    [Tooltip("How much the player's speed is reduced by")]
    public int slowdownFactor = 3;
    [Tooltip("How long it takes to recover 1 exhaustion point, in Seconds")]
    public int timeToRecovery = 1;
    public static int recoverViaPetting = 5;

    private bool isRecovering = false;
    private bool firstTime = true;

    private float referenceSpeed;
    private float slowSpeed;
    private float semislowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (DevMode.devMode == true)
        {
            exhaustionPoints = Mathf.Infinity;
        }
        if (player_0.activeSelf)
        {
            referenceSpeed = player_0.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
        }
        else if (player_1.activeSelf)
        {
            referenceSpeed = player_1.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
        }
        slowSpeed = referenceSpeed / slowdownFactor;
        semislowSpeed = referenceSpeed / Mathf.Sqrt(slowdownFactor);
        exhaustionTimer = distanceToExhaustion;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClickAgentController.PlayerAi == false)
        {
            reduceExhaustionPoints();
        }
        exhaustionEffects();
        FirstRecovery();
        if (firstTime == false)
        {
            recovery();
        }
        if (Exhaustion.exhaustionPoints >= 100)
        {
            playRecoveryAnimation = false;
        }
        IsLeaving();
    }

    void reduceExhaustionPoints()
    {
        if (distanceWalked >= distanceToExhaustion)
        {
            exhaustionPoints -= 1;
            distanceWalked -= distanceToExhaustion;
        }
        if (treesChopped >= treesToExhaustion)
        {
            exhaustionPoints -= 1;
            treesChopped -= treesToExhaustion;
        }
        if (flowersPicked >= flowersToExhaustion)
        {
            exhaustionPoints -= 1;
            flowersPicked -= flowersToExhaustion;
        }
    }

    void exhaustionEffects()
    {
        if (exhaustionPoints <= 0)
        {
            TreeAnimation.chopExhausted = true;
            Flower.flowerExhausted = true;
            if (player_0.activeSelf)
            {
                player = player_0;
            }
            else if (player_1.activeSelf)
            {
                player = player_1;
            }   
            player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = slowSpeed;
        }
        if (exhaustionPoints <= exhaustion1 && exhaustionPoints > recovery1)
        {
            if (player_0.activeSelf)
            {
                player = player_0;
            }
            else if (player_1.activeSelf)
            {
                player = player_1;
            }   
            player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = semislowSpeed;
        }
    }

    void recovery()
    {
        if (player_0.activeSelf)
        {
            player = player_0;
        }
        else if (player_1.activeSelf)
        {
            player = player_1;
        }
        
        if (HomeCloseToPlayer() && exhaustionPoints < 100 && isRecovering == false)
        {
            StartCoroutine(ResetExhaustion());
        }

        if (HomeCloseToPlayer() && exhaustionPoints > recovery1 && exhaustionPoints < 100)
        {
            TreeAnimation.chopExhausted = false;
            Flower.flowerExhausted = false;
            player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = semislowSpeed;
        }
        else if (HomeCloseToPlayer() && exhaustionPoints == 100)
        {
            TreeAnimation.chopExhausted = false;
            Flower.flowerExhausted = false;
            player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = referenceSpeed;
        }
    }

    IEnumerator ResetExhaustion()
    {
        isRecovering = true;
        yield return new WaitForSeconds(timeToRecovery);
        exhaustionPoints += 1;
        isRecovering = false;
    }

    bool HomeCloseToPlayer()
    {
        if (player_0.activeSelf)
        {
            if (Vector2.Distance(player_0.transform.position, playerHome.transform.position+new Vector3(0.3f,0.2f,0)) <= 0.2f || Vector2.Distance(player_0.transform.position, playerHome.transform.position+new Vector3(0f,0.2f,0)) <= 0.2f || Vector2.Distance(player_0.transform.position, playerHome.transform.position+new Vector3(-0.3f,0.2f,0)) <= 0.2f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (player_1.activeSelf)
        {
            if (Vector2.Distance(player_1.transform.position, playerHome.transform.position+new Vector3(0.3f,0.2f,0)) <= 0.2f || Vector2.Distance(player_1.transform.position, playerHome.transform.position+new Vector3(0,0.2f,0)) <= 0.2f || Vector2.Distance(player_1.transform.position, playerHome.transform.position+new Vector3(-0.3f,0.2f,0)) <= 0.2f)
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

    void IsLeaving()
    {
        if (firstTime == false && HomeCloseToPlayer() == false)
        {
            firstTime = true;
            playRecoveryAnimation = false;
        }
    }

    void FirstRecovery()
    {
        if (firstTime == true && HomeCloseToPlayer() == true && isRecovering == false && exhaustionPoints < 100)
        {
            StartCoroutine(FirstRecoveryTimer());
            firstTime = false;
        }       
    }

    IEnumerator FirstRecoveryTimer()
    {
        isRecovering = true;
        yield return new WaitForSeconds(timeToRecovery*5);
        if (HomeCloseToPlayer())
        {
            exhaustionPoints += 1;
            playRecoveryAnimation = true;
            isRecovering = false;
        }
    }
}

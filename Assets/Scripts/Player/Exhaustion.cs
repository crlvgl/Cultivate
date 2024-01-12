using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaustion : MonoBehaviour
{
    public int exhaustionPoints = 100;
    public static float distanceWalked = 0;
    public int distanceToExhaustion = 1000;
    public static int treesChopped = 0;
    public int treesToExhaustion = 5;
    public static int flowersPicked = 0;
    public int flowersToExhaustion = 15;

    public GameObject player_0;
    public GameObject player_1;
    private GameObject player;

    public int recovery1 = 30;
    public int exhaustion1 = 50;

    private float referenceSpeed;
    private float slowSpeed;
    private float semislowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (player_0.activeSelf)
        {
            referenceSpeed = player_0.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
        }
        else if (player_1.activeSelf)
        {
            referenceSpeed = player_1.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
        }
        slowSpeed = referenceSpeed / 3;
        semislowSpeed = referenceSpeed / Mathf.Sqrt(3);
    }

    // Update is called once per frame
    void Update()
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

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Home" && exhaustionPoints < 100)
        {
            StartCoroutine(ResetExhaustion());
        }

        if (other.gameObject.tag == "Home" && exhaustionPoints > recovery1 && exhaustionPoints < 100)
        {
            TreeAnimation.chopExhausted = false;
            Flower.flowerExhausted = false;
            player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = semislowSpeed;
        }
        else if (other.gameObject.tag == "Home" && exhaustionPoints == 100)
        {
            TreeAnimation.chopExhausted = false;
            player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = referenceSpeed;
        }
    }

    IEnumerator ResetExhaustion()
    {
        yield return new WaitForSeconds(1);
        exhaustionPoints += 1;
    }
}

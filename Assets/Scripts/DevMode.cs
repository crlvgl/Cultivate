using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevMode : MonoBehaviour
{
    [Header("Developer Mode Options")]
    public bool sprintMode;
    public static bool devMode = false;
    [Header("Requirements")]
    public GameObject player_0;
    public GameObject player_1;
    public static bool activateSprintV2;

    // Start is called before the first frame update
    void Start()
    {   
        if (sprintMode == true)
        {
            if (player_0.activeSelf)
            {
                player_0.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = player_0.GetComponent<UnityEngine.AI.NavMeshAgent>().speed * 2;
            }
            if (player_1.activeSelf)
            {
                player_1.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = player_1.GetComponent<UnityEngine.AI.NavMeshAgent>().speed * 2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activateSprintV2 == true)
        {
            activateSprintV2 = false;
            player_1.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = player_1.GetComponent<UnityEngine.AI.NavMeshAgent>().speed * 2;
        }
    }
}

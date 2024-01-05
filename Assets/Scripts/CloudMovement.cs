using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float deadZoneX;
    public float speedMin;
    public float speedMax;
    private float randomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        randomSpeed = Random.Range(speedMin, speedMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(deadZoneX, transform.position.y), randomSpeed * Time.deltaTime);
        if (transform.position.x == deadZoneX)
        {
            Destroy(gameObject);
            CloudSpawner.cloudCount--;
        }
    }
}

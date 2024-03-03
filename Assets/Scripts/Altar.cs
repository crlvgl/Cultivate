using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{

    public GameObject Player1;
    public GameObject Player2;

    public GameObject AltarLitUp;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerCloseToAltar())
        {
            Inventory.Altar = 1;
            StartCoroutine(ActivateAltar());
        }
    }

    IEnumerator ActivateAltar()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(2.31f);
        AltarLitUp.SetActive(true);
        this.gameObject.SetActive(false);
    }

    bool IsPlayerCloseToAltar()
    {
        if (Player1.activeSelf)
        {
            float distance = Vector2.Distance(Player1.transform.position, this.transform.position);
            return distance <= 1f;
        }
        else if (Player2.activeSelf)
        {
            float distance = Vector2.Distance(Player2.transform.position, this.transform.position);
            return distance <= 1f;
        }
        else
        {
            return false;
        }
    }
}

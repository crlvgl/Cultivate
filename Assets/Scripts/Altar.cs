using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{

    public GameObject Player1;
    public GameObject Player2;

    public GameObject AltarLitUp;
    public GameObject AltarCanvas;

    public Animator animator;
    private bool notPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.enabled = false;

        AltarCanvas = AltarLitUp.transform.Find("AltarCanvas").gameObject;
        if (AltarCanvas == null)
        {
            Debug.Log("AltarCanvas not found");
        }
        else
        {
            Debug.Log(AltarCanvas.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerCloseToAltar() && notPlaying)
        {
            Inventory.Altar = 1;
            StartCoroutine(ActivateAltar());
        }
    }

    IEnumerator ActivateAltar()
    {
        notPlaying = false;
        animator.enabled = true;
        yield return new WaitForSeconds(2.31f);
        AltarLitUp.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        this.transform.Find("Pillars Old").gameObject.SetActive(true);
        this.transform.Find("Pillars").gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        AltarCanvas.SetActive(true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    public bool isAlive = true;
    public Transform escapeDoor;
    Rigidbody2D rb;
    public float runSpeed;
    GameController gc;
    Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(1).GetComponent<Animator>();
    }

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.isFinalRun)
        {
            //anim.SetBool("isRunning", true);
            FinalRun();
        }
    }

    public void FinalRun()
    {
        Vector3 direction = escapeDoor.position - transform.position;
        rb.velocity = direction.normalized * runSpeed;
    }
}

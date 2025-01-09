using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEnemy : MonoBehaviour
{
    //public bool isAlive = true;
    public Transform escapeDoor;
    Rigidbody2D rb;
    public float runSpeed;
    GameController gc;
    Animator anim;
    public int index;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //Debug.Log(gc.isFinalRun);
        if (gc.isFinalRun[index])
        {
            //anim.SetBool("isRunning", true);
            FinalRun();
            //Debug.Log("111");
        }
    }

    public void FinalRun()
    {
        Vector3 direction = escapeDoor.position - transform.position;
        //rb.velocity = direction.normalized * runSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + direction.normalized * runSpeed * Time.deltaTime);
    }
}

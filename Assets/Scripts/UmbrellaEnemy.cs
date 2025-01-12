using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaEnemy : MonoBehaviour
{
    public Transform escapeDoor;
    Rigidbody2D rb;
    public float runSpeed;
    GameController gc;
    Animator anim;
    //new set
    //
    public int index;
    bool paused = false;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(1).GetComponent<Animator>();
    }

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            return;
        }
      
        //



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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rain")
        {
            anim.SetBool("inRain", true);
            this.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rain")
        {
            anim.SetBool("inRain", false);
            this.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

}

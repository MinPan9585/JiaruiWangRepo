using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaEnemyWalk : MonoBehaviour
{
    public Transform escapeDoor;
    Rigidbody2D rb;
    public float runSpeed;
    GameController gc;
    Animator anim;
    //new set
    public GameObject Point1;
    public GameObject Point2;
    private Transform currentPoint;
    public float WalkSpeed;
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
        //new set
        currentPoint = Point2.transform;
        anim.SetBool("isWalking", true);
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            return;
        }
        //new set
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == Point2.transform)
        {
            rb.velocity = new Vector2(WalkSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-WalkSpeed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Point2.transform)
        {
            paused = true;
            anim.SetBool("isWalking", false);
            flip();
            currentPoint = Point1.transform;
            StartCoroutine(PauseSeconds());
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Point1.transform)
        {
            paused = true;
            anim.SetBool("isWalking", false);
            flip();
            currentPoint = Point2.transform;
            StartCoroutine(PauseSeconds());
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

    IEnumerator PauseSeconds()
    {
        yield return new WaitForSeconds(2f);
        paused = false;
        anim.SetBool("isWalking", true);
    }

    public void FinalRun()
    {
        Vector3 direction = escapeDoor.position - transform.position;
        //rb.velocity = direction.normalized * runSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + direction.normalized * runSpeed * Time.deltaTime);
    }

    private void flip()
    {
        Vector3 LocalScale = transform.localScale;
        LocalScale.x *= -1;
        transform.localScale = LocalScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Point1.transform.position, 0.5f);
        Gizmos.DrawWireSphere(Point2.transform.position, 0.5f);
        Gizmos.DrawLine(Point1.transform.position, Point2.transform.position);
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

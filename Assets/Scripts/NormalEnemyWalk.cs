using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyWalk : MonoBehaviour
{
    public bool isAlive = true;
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
            flip();
            currentPoint = Point1.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Point1.transform)
        {
            flip();
            currentPoint = Point2.transform;
        }
        //



        //Debug.Log(gc.isFinalRun);
        if (gc.isFinalRun)
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
}

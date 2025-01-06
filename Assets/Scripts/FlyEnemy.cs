using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    GameController gc;
    Animator anim;
    public GameObject Point1;
    public GameObject Point2;
    private Transform currentPoint;
    public float WalkSpeed;
    EnemyHealth health;
    
    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(1).GetComponent<Animator>();
    }
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        //new set
        currentPoint = Point2.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(health.isAlive == false)
        {
            return;
        }
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
            //Debug.Log("111111");
            Flip();
            currentPoint = Point1.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Point1.transform)
        {
            Flip();
            currentPoint = Point2.transform;
        }

        
    }
    private void Flip()
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

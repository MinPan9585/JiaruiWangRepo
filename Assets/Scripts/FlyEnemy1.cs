using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy1 : MonoBehaviour
{
    Rigidbody2D rb;
    GameController gc;
    Animator anim;
    public GameObject[] Points;
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
        currentPoint = Points[1].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.isAlive == false)
        {
            return;
        }
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == Points[1].transform)
        {
            rb.velocity = new Vector2(WalkSpeed, 0);

        }
        else if (currentPoint == Points[2].transform)
        {
            rb.velocity = new Vector2(0, -WalkSpeed);
        }
        else if (currentPoint == Points[3].transform)
        {
            rb.velocity = new Vector2(-WalkSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, WalkSpeed);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Points[2].transform)
        {
            //Debug.Log("111111");
            //Flip();
            currentPoint = Points[3].transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Points[3].transform)
        {
            //Flip();
            currentPoint = Points[0].transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Points[0].transform)
        {
            //Flip();
            currentPoint = Points[1].transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Points[1].transform)
        {
            //Flip();
            currentPoint = Points[2].transform;
        }



    }
    private void Flip()
    {
        Vector3 LocalScale = transform.localScale;
        LocalScale.x *= -1;
        transform.localScale = LocalScale;
    }
}

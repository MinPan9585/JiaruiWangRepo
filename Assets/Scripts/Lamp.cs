using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = transform.GetChild(1).GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<EnemyHealth>().isAlive)
        {
            collision.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Die");
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("GameController").GetComponent<GameController>().score += 1;
            collision.gameObject.GetComponent<EnemyHealth>().isAlive = false;
            //Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isGrounded", true);
        }
    }
}

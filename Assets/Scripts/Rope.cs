using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            transform.parent.GetChild(1).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //GetComponent<Animator>().SetTrigger("Cut");
            anim.SetBool("isBroken", true);
        }
    }   
}

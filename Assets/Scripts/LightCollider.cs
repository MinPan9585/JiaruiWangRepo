using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollider : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = transform.parent.GetChild(1).GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            // enemy start run
            GameObject.Find("GameController").GetComponent<GameController>().isFinalRun = true;
            anim.SetBool("isRunning", true);    
        }
    }
}

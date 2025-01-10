using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollider : MonoBehaviour
{
    //Animator anim;
    public int index;
    public Animator[] anims;

    private void Awake()
    {
        //anim = transform.parent.GetChild(1).GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            // enemy start run
            GameObject.Find("GameController").GetComponent<GameController>().isFinalRun[index] = true;
            foreach (var anim in anims)
            {
                anim.SetBool("isRunning", true);
            }
            //anim.SetBool("isRunning", true);    
        }
    }
}

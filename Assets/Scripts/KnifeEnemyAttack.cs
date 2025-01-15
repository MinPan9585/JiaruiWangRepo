using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEnemyAttack : MonoBehaviour
{
    AudioSource audioS;
    public AudioClip[] sfx;
    // public float speed;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            audioS.PlayOneShot(sfx[0]);
            print("111");
            this.transform.parent.GetChild(0).GetChild(1).GetComponent<Animator>().SetTrigger("Attack");
            // other.GetComponent<Rigidbody2D>().velocity = new Vector2(this.transform.parent.localScale.x * speed , 0);
        }
    }

}

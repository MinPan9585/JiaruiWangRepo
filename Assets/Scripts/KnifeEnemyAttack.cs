using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEnemyAttack : MonoBehaviour
{
    // public float speed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            print("111");
            this.transform.parent.GetChild(0).GetChild(1).GetComponent<Animator>().SetTrigger("Attack");
            // other.GetComponent<Rigidbody2D>().velocity = new Vector2(this.transform.parent.localScale.x * speed , 0);
        }
    }

}

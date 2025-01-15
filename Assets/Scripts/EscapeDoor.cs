using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDoor : MonoBehaviour
{
    public AudioClip[] sfx;
    AudioSource audioS;
    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<EnemyHealth>().isAlive)
        {

            if (collision.transform.childCount == 3)
            {
                collision.transform.GetChild(2).gameObject.SetActive(false);
            }
            if (collision.transform.parent.childCount == 2)
            {
                //Debug.Log("000000000000000000000");
                collision.transform.parent.GetChild(1).gameObject.SetActive(false);
            }

            audioS.PlayOneShot(sfx[0]);
            Destroy(collision.transform.gameObject, 1);
        }
    }
}

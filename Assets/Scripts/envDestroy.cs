using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class envDestroy : MonoBehaviour
{
    //AudioSource audioS;
    //public AudioClip[] sfx;
    public GameObject destroyVFX;
    public GameObject destroySFX;
    private void Awake()
    {
        //audioS = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        if (destroyVFX != null)
        {
            //audioS.PlayOneShot(sfx[0]);
            Instantiate(destroyVFX, transform.position, transform.rotation);
            Instantiate(destroySFX, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}

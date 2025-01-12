using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class envDestroy : MonoBehaviour
{
    public GameObject destroyVFX;


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
            Instantiate(destroyVFX, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}

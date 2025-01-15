using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    public PolygonCollider2D poly;
    AudioSource audioS;
    public AudioClip[] sfx;

    // Start is called before the first frame update
    private void Awake()
    {
        //anim = transform.parent.GetChild(1).GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //poly = GetComponentInChildren<PolygonCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Explode();
        }
    }

    void Explode()
    {
        audioS.PlayOneShot(sfx[0]);
        GetObjectsInsidePolygon();
        anim.SetTrigger("Explode");
    }

    void DestroyBarrel()
    {
        Destroy(gameObject);
    }

    void GetObjectsInsidePolygon()
    {
        Collider2D[] results = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;

        // Set the layer mask to filter specific layers
        filter.SetLayerMask(LayerMask.GetMask("Enemy"));

        int colliderCount = Physics2D.OverlapCollider(poly, filter, results);
        for (int i = 0; i < colliderCount; i++)
        {
            //Debug.Log(results[i].name);
            results[i].transform.GetChild(1).GetComponent<Animator>().SetTrigger("Die");
            Destroy(results[i].gameObject, 1);
        }
    }

    void GetOtherBarrelsInsidePolygon()
    {
        Collider2D[] results = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;

        // Set the layer mask to filter specific layers
        filter.SetLayerMask(LayerMask.GetMask("Barrel"));

        int colliderCount = Physics2D.OverlapCollider(poly, filter, results);
        for (int i = 0; i < colliderCount; i++)
        {
            //Debug.Log(results[i].name);
            results[i].GetComponent<ExplosiveBarrel>().Explode();
        }
    }
}

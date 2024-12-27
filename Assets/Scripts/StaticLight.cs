using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            // enemy start run
            GameObject.Find("GameController").GetComponent<GameController>().isFinalRun = true;
        }
    }
}


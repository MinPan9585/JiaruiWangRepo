using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    GameController gc;
    public GameObject arrow;
    public bool hasArrow = true;
    public bool arrowExist = false;
    Animator anim;
    //public Animator anim;

    Vector3 mousePosition;
    Vector3 finalMousePos;
    Vector3 finalDir;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnArrow());
        gc = GameObject.Find("GameController").GetComponent<GameController>();  
    }

    private void Update()
    {
        if (!hasArrow)
        {
            if (Input.GetKeyDown(KeyCode.R) && gc.finishReload)
            {
                gc.finishReload = false;
                StartCoroutine(SpawnArrow());
                hasArrow = true;
            }
        }
    }

    //public void SpawnArrow()
    //{
    //    // spawn ui to pick direction
    //    Debug.Log("111");

    //    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    finalMousePos = new Vector3(mousePosition.x, mousePosition.y, 0);

    //    Vector3 direction = finalMousePos - transform.position;

    //    float angle = Mathf.Atan2(finalDir.y, finalDir.x) * Mathf.Rad2Deg - 90f;
    //    Quaternion finalRotation = Quaternion.Euler(new Vector3(0, 0, angle));


    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log("222");
    //        Instantiate(arrow, transform.position, finalRotation);
    //    }
    //}

    IEnumerator SpawnArrow()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        while (arrowExist == false)
        {
            // spawn ui to pick direction
            //Debug.Log("111");

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            finalMousePos = new Vector3(mousePosition.x, mousePosition.y, 0);

            Vector3 direction = finalMousePos - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion finalRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            transform.GetChild(0).rotation = finalRotation;

            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("222");
                Instantiate(arrow, transform.position, finalRotation);
                transform.GetChild(0).gameObject.SetActive(false);
                arrowExist = true;
                //anim.SetTrigger("Shot");
                anim.SetBool("isshot", true);
            }
            yield return null;
        }
        
    }
}

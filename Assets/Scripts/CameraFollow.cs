using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float halfWindowWidth = 21.335f;
    float halfWindowHeight = 12f; 

    // Update is called once per frame
    void Update()
    {
        GameObject arrow = GameObject.Find("Arrow(Clone)");

        if (arrow != null)
        {
            if (arrow.transform.position.x > halfWindowWidth)
            {
                transform.position = new Vector3(halfWindowWidth * 2 , transform.position.y, transform.position.z);
            }
            else if (arrow.transform.position.x < -halfWindowWidth)
            {
                transform.position = new Vector3(-halfWindowWidth * 2, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }
    }
}

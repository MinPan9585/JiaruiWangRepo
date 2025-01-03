using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    float halfWindowWidth = 21.335f;
    float halfWindowHeight = 12f;
    int horizontalNumber;
    int verticalNumber;

    // Update is called once per frame
    void Update()
    {
        GameObject arrow = GameObject.Find("Arrow(Clone)");

        if (arrow != null)
        {
            for (int i = 0; i < 10; i++)
            {
                if (arrow.transform.position.x - 2 * i * halfWindowWidth < 0)
                {
                    horizontalNumber = i;
                    break;
                }
            }
            for (int j = 0; j < 10; j++)
            {
                if (arrow.transform.position.y - 2 * j * halfWindowHeight < 0)
                {
                    verticalNumber = j;
                    break;
                }
            }
            transform.position = new Vector3(halfWindowWidth * (horizontalNumber * 2 - 1), 
                halfWindowHeight * (verticalNumber * 2 - 1), transform.position.z);
        }

        
    }
}

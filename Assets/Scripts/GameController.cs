using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score = 0;
    void Update()
    {
        if(score >= 5)
        {
            Debug.Log("You win!");
        }
    }
}

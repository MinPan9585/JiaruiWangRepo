using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public bool isFinalRun = false;
    public bool isFinalRun2 = false;
    public bool isFinalRun3 = false;
    public bool isFinalRun4 = false;
    public bool isFinalRun5 = false;
    public bool isFinalRun6 = false;
    public bool finishReload = false;

    void Update()
    {
        if(score >= 5)
        {
            Debug.Log("You win!");
        }
    }
}

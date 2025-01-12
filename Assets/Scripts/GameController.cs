using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public bool[] isFinalRun;
    public bool finishReload = false;
    public bool redoReload = false;
    public GameObject reload;

    void Update()
    {
        if(score >= 5)
        {
            Debug.Log("You win!");
        }

        if(redoReload)
        {
            Debug.Log("22222222222");
            Instantiate(reload, Vector3.zero, Quaternion.identity);
            redoReload = false;

        }
    }
}

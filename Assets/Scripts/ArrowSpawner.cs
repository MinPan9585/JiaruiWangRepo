using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public bool hasArrow = true;

    // Start is called before the first frame update
    void Start()
    {
        SpawnArrow();
    }

    private void Update()
    {
        if (!hasArrow)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SpawnArrow();
                hasArrow = true;
            }
        }
    }

    public void SpawnArrow()
    {
        Instantiate(arrow, transform.position, Quaternion.identity);
    }
}

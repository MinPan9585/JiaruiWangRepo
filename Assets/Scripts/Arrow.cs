using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arrow : MonoBehaviour
{
    public float speed;
    Vector3 mousePosition;
    Vector3 finalMousePos;
    Vector3 lastFrameDir;
    public float rotateSpeed;
    public GameObject deathvfx;
    public GameObject hitGroundVFX;
    bool isFlying = true;
    

    void Update()
    {
        if (isFlying)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            finalMousePos = new Vector3(mousePosition.x, mousePosition.y, 0);

            Vector3 direction = finalMousePos - transform.position;
            Vector3 finalDir = Vector3.Lerp(direction, lastFrameDir, rotateSpeed);

            transform.Translate(finalDir.normalized * speed * Time.deltaTime, Space.World);

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(finalDir.y, finalDir.x) * Mathf.Rad2Deg - 90f;

            // Apply the rotation to the arrow
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            lastFrameDir = finalDir;
        }
        else
        {
            return;
        }
        

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Instantiate(deathvfx, collision.transform.position, Quaternion.identity);
        }
        if (collision.gameObject.tag == "Ground")
        {
            isFlying = false;
            Instantiate(hitGroundVFX, transform.position, transform.rotation);
            GameObject.Find("ArrowSpawner").GetComponent<ArrowSpawner>().hasArrow = false;
            Destroy(gameObject, 1f);
        }
    }

}

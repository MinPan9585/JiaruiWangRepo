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
            // Move the arrow in the direction of the mouse position
            transform.Translate(transform.up.normalized * speed * Time.deltaTime, Space.World);

            // Calculate the angle in degrees between the arrow's current direction and the direction to the mouse position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

            // Get the current angle of the arrow
            float currentAngle = transform.rotation.eulerAngles.z;

            // Calculate the difference between the current angle and the target angle
            float angleDifference = Mathf.DeltaAngle(currentAngle, angle);

            // Determine the rotation direction and apply the rotation
            if (angleDifference > 0)
            {
                transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
            }
            else if (angleDifference < 0)
            {
                transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            }



            //Vector3 finalDir = Vector3.Lerp(direction, lastFrameDir, rotateSpeed);

            //transform.Translate(finalDir.normalized * speed * Time.deltaTime, Space.World);

            //// Calculate the angle in degrees
            //float angle = Mathf.Atan2(finalDir.y, finalDir.x) * Mathf.Rad2Deg - 90f;

            //// Apply the rotation to the arrow
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            //lastFrameDir = finalDir;
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

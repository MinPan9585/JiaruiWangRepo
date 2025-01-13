using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public GameObject hitGroundSFX;
    AudioSource audioS;
    public AudioClip[] sfx;
    public float speed;
    Vector3 mousePosition;
    Vector3 finalMousePos;
    Vector3 lastFrameDir;
    public float rotateSpeed;
    public GameObject deathvfx;
    public GameObject hitGroundVFX;
    bool isFlying = true;
    GameObject arrowSpawner;
    GameController gc;
    public GameObject reload;
    public CameraShake cs;
    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
        cs = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        arrowSpawner = GameObject.Find("ArrowSpawner");
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

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

        if (Input.GetMouseButton(0))
        {
            speed = 10;
            rotateSpeed = 10;
        }
        if (Input.GetMouseButtonUp(0))
        {
            speed = 5;
            rotateSpeed = 270;
        }
     
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<EnemyHealth>().isAlive)
        {
            audioS.PlayOneShot(sfx[1]);
            collision.GetComponent<Rigidbody2D>().AddForce(transform.up * 10, ForceMode2D.Impulse);
            collision.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Die");
            collision.gameObject.GetComponent<EnemyHealth>().isAlive = false;
            if (collision.transform.childCount == 3)
            {
                collision.transform.GetChild(2).gameObject.SetActive(false);
            }
            if(collision.transform.parent.childCount == 2)
            {
                //Debug.Log("000000000000000000000");
                collision.transform.parent.GetChild(1).gameObject.SetActive(false);
            }
            

            Destroy(collision.transform.gameObject, 1);
            Instantiate(deathvfx, collision.transform.position, Quaternion.identity);
            GameObject.Find("GameController").GetComponent<GameController>().score += 1;
            collision.GetComponent<Rigidbody2D>().isKinematic = false;

        }
        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(hitGroundSFX, transform.position, Quaternion.identity);
            isFlying = false;
            cs.Shake();
            Instantiate(hitGroundVFX, transform.position, transform.rotation);
            //Instantiate(brokenArrow, transform,position, transform.rotation);
            GameObject.Find("ArrowSpawner").GetComponent<ArrowSpawner>().hasArrow = false;
            Destroy(gameObject);
            arrowSpawner.GetComponent<ArrowSpawner>().arrowExist = false;
            //gc.transform.GetChild(0).gameObject.SetActive(true);
            Instantiate(reload, Vector3.zero, Quaternion.identity);
        }
    }

    

}

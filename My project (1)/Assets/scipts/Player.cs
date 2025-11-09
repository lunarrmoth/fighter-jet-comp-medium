using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float playerspeed; 
    private float horizontalinput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;
    private float verticalScreenLimit = 6.5f;
     public GameObject bulletPrefab;


        void Start()
    {
        playerspeed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement ();
        Shooting ();
    }
     void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    void Movement ()
    {
        //get player input
        horizontalinput = Input.GetAxis ("Horizontal");
        verticalInput = Input.GetAxis ("Vertical");
        //move the player
       transform.Translate(new Vector3(horizontalinput, verticalInput, 0) * Time.deltaTime * playerspeed);
        //Player leaves the screen horizontally
        if(transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        //Player leaves the screen vertically
        if(transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }
   }
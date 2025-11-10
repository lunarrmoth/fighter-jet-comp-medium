using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Timeline;
public class Player : MonoBehaviour
{
    public int lives;
    private float speed;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 5.0f;
        // gameManager.ChangeLivesText(lives);
    }
    void Update()
    {
        Shooting();
        Movement();
    }
    // Update is called once per frame

    public void LoseALife()
    {
        lives = lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //Object.Destroy(this.GameObject);

        }
    }

    public void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    public void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(horizontalInput, 0, 0) * Time.deltaTime * speed, Space.World);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        Vector3 pos = transform.position;

        if (pos.x <= -horizontalScreenSize || pos.x > horizontalScreenSize)
        {
            pos.x = -pos.x;
        }

        transform.position = pos;
    }
}




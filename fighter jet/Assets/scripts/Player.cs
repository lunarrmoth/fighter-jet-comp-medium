using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lives;
    private float playerSpeed;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    void Start()
    {
        GameObject gmObj = GameObject.Find("GameManager");
        if (gmObj != null)
        {
            gameManager = gmObj.GetComponent<GameManager>();
        }
        else
        {
            Debug.LogError("GameManager not found in scene!");
        }

        lives = 3;
        playerSpeed = 5.0f;
    }

    void Update()
    {
        Shooting();
        Movement();
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(horizontalInput, 0, 0) * Time.deltaTime * playerSpeed, Space.World);

        // Only use gameManager if it exists
        if (gameManager != null)
        {
            float horizontalScreenSize = gameManager.horizontalScreenSize;
            float verticalScreenSize = gameManager.verticalScreenSize;

            Vector3 pos = transform.position;

            if (pos.x <= -horizontalScreenSize || pos.x > horizontalScreenSize)
            {
                pos.x = -pos.x;
            }

            if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
            }

            transform.position = pos;
        }
        else
        {
            // Fallback values if GameManager not found
            float horizontalScreenSize = 10f;
            float verticalScreenSize = 6.5f;

            Vector3 pos = transform.position;

            if (pos.x <= -horizontalScreenSize || pos.x > horizontalScreenSize)
            {
                pos.x = -pos.x;
            }

            if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
            }

            transform.position = pos;
        }
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Timeline;
public class Player 
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

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        lives = lives - 1;
        lives -= 1;
        lives--;
       gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Object.Instantiate(explosionPrefab, Transform.position, Quaternion.identity);
           // Object.Destroy(this.GameObject);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Object.Instantiate(bulletPrefab, Transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (Transform.position.x <= -horizontalScreenSize || Transform.position.x > horizontalScreenSize)
        {
            Transform.position = new Vector3(Transform.position.x * -1, Transform.position.y, 0);
        }

        if (Transform.position.y <= -verticalScreenSize || Transform.position.y > verticalScreenSize)
        {
            Transform.position = new Vector3(Transform.position.x, Transform.position.y * -1, 0);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Timeline;
public class Player : MonoBehaviour
{
    public int lives;
    //public int weaponType;
    private float playerSpeed;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    //public GameObject thrusterPrefab;
    //public GameObject ShieldPrefab;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        playerSpeed = 5.0f;
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

    // IEnumerator SpeedPowerDown()
    //   {
    //      yield return new WaitForSeconds(3f);
    //      playerSpeed = 6f;
    //      thrusterPrefab.SetActive(false);
    //      gameManager.ManagePowerupText(0);
    //      gameManager.PlaySound(2);
    //  }

    //  private void OnTriggerEnter2D(Collider2D whatDidIHit)
    //{
    //  if (whatDidIHit.tag == "Powerup")
    //   {
    //      Destroy(whatDidIHit.gameObject);
    //      int whichPowerup = Random.Range(1, 5);
    //      gameManager.PlaySound(1);
    //      switch (whichPowerup)
    //      {
    //          case 1:
    //             playerSpeed = 10f;
    //             //start coroutine 
    //             StartCoroutine(SpeedPowerDown());
    //             thrusterPrefab.SetActive(true);
    //             gameManager.ManagePowerupText(1);
    //             break;
    //         case 2:
    //             weaponType = 2;
    //                    StartCoroutine(WeaponPowerDown());
    //             gameManager.ManagePowerupText(2);
    //             break;
    //          case 3:
    //             weaponType = 3;
    //             StartCoroutine(WeaponPowerDown());
    //             gameManager.ManagePowerupText(3);
    //             break;
    //         case 4:
    // shield powerup do you have shield if yes do nothing if not activate it 
    //         gameManager.ManagePowerupText(4);
    //          break;
    //  }
    //   }
    //   }

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




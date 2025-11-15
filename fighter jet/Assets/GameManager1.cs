using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject cloudPrefab;
    public GameObject powerupPrefab;
    //public GameObject gameOverText;
    //public GameObject restartText;
    public GameObject audioPlayer;// 1 audio source 2 an audio clip 
                                  // public AudioClip powerUpSound;
                                  // public AudioClip powerDownSound;

    // legacy, TMPro
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpText;
    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;

        CreateSky();
        InvokeRepeating("CreateEnemy", 1f, 3f);

        InvokeRepeating("CreateEnemyTwo", 15f, 3f);
    }
    void CreateEnemy()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }

    }
    //this one is copied from class 
    public void AddScore(int earnedScore)
    {
        score += earnedScore;// score = score + earnedScore;
        scoreText.text = "Score: " + score;
    }

    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-verticalScreenSize * 0.8f, verticalScreenSize * 0.8f), 0), Quaternion.identity);
    }
    // void Update()
    // {
    //     if (gameOver && Input.GetKeyDown(KeyCode.R))
    //     {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //     }
    //     // load scene needs a string of a scene that is in build settings
    //     // this will not add a new scene
    // }
    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerUpText.text = "Speed!";

                break;
            case 2:
                powerUpText.text = "Double Weapon!";

                break;
            case 3:
                powerUpText.text = "Triple Weapon!";
                break;
            case 4:
                powerUpText.text = "Shield!";
                break;
            default:
                powerUpText.text = "No Powers yet!";
                break;
        }
    }

    // public void PlaySound(int whichSound)
    // {
    //     switch (whichSound)
    //     {
    //         case 1:
    //             audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
    //             break;
    //         case 2:
    //             audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
    //             break;

    //     }
    // }
    //     public void GameOver()
    //     {
    //         gameOverText.SetActive(true);
    //         restartText.SetActive(true);
    //         gameOver = true;
    //         CancelInvoke();
    //         cloudMove = 0;
    //     }

}


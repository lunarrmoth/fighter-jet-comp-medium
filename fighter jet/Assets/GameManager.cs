using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject cloudPrefab;

    public TextMeshProUGUI livesText;

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

    // Update is called once per frame
    void Update()
    {

    }

    void CreateEnemy()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) , verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }

    }
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
    }

    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
}


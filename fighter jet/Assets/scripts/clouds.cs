using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speed;
    private GameManager gameManager;

    void Start()
    {
        // Find GameManager
        GameObject gmObj = GameObject.Find("GameManager");
        if (gmObj != null)
        {
            gameManager = gmObj.GetComponent<GameManager>();
        }
        else
        {
            Debug.LogError("GameManager GameObject not found in scene!");
        }

        // Set random cloud properties
        transform.localScale = transform.localScale * Random.Range(0.1f, 0.6f);
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Random.Range(0.1f, 0.7f));
        speed = Random.Range(3f, 7f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Only use gameManager if it exists
        if (gameManager != null)
        {
            if (transform.position.y < -gameManager.verticalScreenSize)
            {
                transform.position = new Vector3(
                    Random.Range(-gameManager.horizontalScreenSize, gameManager.horizontalScreenSize),
                    gameManager.verticalScreenSize * 1.2f,
                    0
                );
            }
        }
        else
        {
            // Fallback if GameManager not found - use hardcoded values
            if (transform.position.y < -6.5f)
            {
                transform.position = new Vector3(Random.Range(-10f, 10f), 7.8f, 0);
            }
        }
    }
}
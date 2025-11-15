using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameManager gameManager;
    public float bulletSpeed = 8f;
    private float verticalScreenLimit = 6.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject gmObj = GameObject.Find("GameManager");
        if (gmObj != null)
        {
            gameManager = gmObj.GetComponent<GameManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hi I hit somethin");
        if (other == null) return;

        if (other.gameObject.tag == "Enemy")
        {
            // Explosion at enemy position
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            }
            // Destroy enemy and bullet
            Destroy(other.gameObject);  // Destroy the enemy
            Destroy(gameObject);  // Destroy the bullet
        }
    }

    // Update is called once per frame
    public void Update()
    {
        // Move bullet upward
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        // Destroy bullet when it goes off screen
        if (transform.position.y > verticalScreenLimit)
        {
            Destroy(gameObject);
        }
    }
}
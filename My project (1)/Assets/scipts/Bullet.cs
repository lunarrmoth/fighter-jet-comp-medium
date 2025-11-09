using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 8f;
    private float verticalScreenLimit = 6.5f;

    void Update()
    {
        // Move bullet upward
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        
        // Destroy bullet when it goes off screen
        if (transform.position.y > verticalScreenLimit)
        {
            Destroy(gameObject);
        }
    }

    // This is called when the bullet collides with something
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we hit an enemy
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);  // Destroy the enemy
            Destroy(gameObject);  // Destroy the bullet
        }
    }
}
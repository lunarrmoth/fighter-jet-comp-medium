using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Enemy : MonoBehaviour
{

    public GameObject explosionPrefab;

    private Vector3 startPosition;
    private GameManager gameManager;
    [Header("Patrol")]
    [SerializeField] private float speed = 2.0f;       // cycles per second factor
    [SerializeField] private float moveRange = 3.0f;   // half-width of left/right travel from start position

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Enemy";
        startPosition = transform.position;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, moveRange * 2f) - moveRange;
        transform.position = new Vector3(startPosition.x + offset, transform.position.y, transform.position.z);
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 3f);
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy hit by: " + other.gameObject.name);

        // Check if it was hit by a bullet
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);  // Destroy the bullet
            Destroy(this.gameObject);   // Destroy the enemy
        }
    }
}

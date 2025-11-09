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
    }
    public void LoseALife()
    {
    }
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<Player>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (whatDidIHit.tag == "Bullet")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}

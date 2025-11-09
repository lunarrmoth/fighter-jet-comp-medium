using UnityEditor.Timeline;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    public GameObject explosionPrefab;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        GameObject gmObj = GameObject.Find("GameManager");
        if (gmObj != null)
        {
            gameManager = gmObj.GetComponent<GameManager>();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        if (other.CompareTag("Enemy"))
        {
            // Explosion at enemy position
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            }



            // Destroy enemy and bullet
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

        // Update is called once per frame
       public void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 8f);
        if (transform.position.y > 6.5f)
        {
            Destroy(gameObject);
        }
    }
}

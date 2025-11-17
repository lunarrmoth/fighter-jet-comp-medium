using System.Collections;
using UnityEngine;

public class Coin_script : MonoBehaviour
{
    [SerializeField] private float lifetimeSeconds = 5f;

    private GameManager gameManager;
    private ScoreManager scoreManager;
    public GameObject coinPrefab;

    void Start()
    {
        var gmObj = GameObject.Find("GameManager");
        if (gmObj != null)
            gameManager = gmObj.GetComponent<GameManager>();
        else
            Debug.LogWarning("GameManager not found in scene – using fallback spawn ranges.");

        // Find the ScoreManager component in the scene
        scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager == null)
            Debug.LogWarning("ScoreManager component not found in scene!");

        // Determine sprite/collider extents so the coin does not spawn partially off-screen
        Vector2 halfSize = Vector2.zero;
        var col = GetComponent<Collider2D>();
        if (col != null)
            halfSize = col.bounds.extents;
        else
        {
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                halfSize = sr.bounds.extents;
        }

        Vector3 spawn;
        if (gameManager != null)
        {
            // Ensure GameManager sizes are positive and compute safe spawn ranges that account for the coin's half-size
            float hSize = Mathf.Abs(gameManager.horizontalScreenSize);
            float vSize = Mathf.Abs(gameManager.verticalScreenSize);

            float maxX = Mathf.Max(0.01f, hSize - halfSize.x);
            float maxY = Mathf.Max(0.01f, vSize - halfSize.y);

            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(-maxY, maxY);
            spawn = new Vector3(x, 0, 0);
        }
        else
        {
            // Fallback ranges (account for half-size)
            float maxX = Mathf.Max(0.5f, 5f - halfSize.x);
            float maxY = Mathf.Max(0.5f, 3f - halfSize.y);
            spawn = new Vector3(Random.Range(-maxX, maxX), 0, 0);
        }

        transform.position = spawn;

        StartCoroutine(SelfDestruct(lifetimeSeconds));
    }

    private IEnumerator SelfDestruct(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Add score when player collects coin
            if (scoreManager != null)
            {
                scoreManager.AddScore(1);
            }

            Destroy(gameObject);
        }
    }
}
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float lifetimeSeconds = 5f;

    private GameManager gameManager;
    public GameObject coinPrefab;
    void Start()
    {
        var gmObj = GameObject.Find("GameManager");
        if (gmObj != null)
            gameManager = gmObj.GetComponent<GameManager>();
        else
            Debug.LogWarning("GameManager not found in scene — using fallback spawn ranges.");

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
            spawn = new Vector3(x, y, transform.position.z);
        }
        else
        {
            // Fallback ranges (account for half-size)
            float maxX = Mathf.Max(0.5f, 5f - halfSize.x);
            float maxY = Mathf.Max(0.5f, 3f - halfSize.y);
            spawn = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), transform.position.z);
        }

        transform.position = spawn;

        StartCoroutine(selfDestruct(lifetimeSeconds));
    }

    private IEnumerator selfDestruct(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
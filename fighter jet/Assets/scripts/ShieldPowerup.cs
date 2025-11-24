using System.Collections;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    [SerializeField] private float lifetimeSeconds = 5f;
    private GameManger GameManger;

    void Start()
    {
        var gmObj = GameObject.Find("GameManger");
        if (gmObj != null)
            GameManger = gmObj.GetComponent<GameManger>();
        else
            Debug.LogWarning("GameManger not found in scene – using fallback spawn ranges.");

        // Determine sprite/collider extents so the powerup does not spawn partially off-screen
        Vector2 halfSize = Vector2.zero;
        var col = GetComponent<Collider>();
        if (col != null)
            halfSize = col.bounds.extents;
        else
        {
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                halfSize = sr.bounds.extents;
        }

        Vector3 spawn;
        if (GameManger != null)
        {
            float hSize = Mathf.Abs(GameManger.horizontalScreenSize);
            float vSize = Mathf.Abs(GameManger.verticalScreenSize);

            float maxX = Mathf.Max(0.01f, hSize - halfSize.x);
            float maxY = Mathf.Max(0.01f, vSize - halfSize.y);

            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(-maxY, maxY);
            spawn = new Vector3(x, 0, 0);
        }
        else
        {
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
            // Get the Player component and activate shield
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.ActivateShield();
            }

            // Show power-up text
            PowerUpTextManager textManager = FindFirstObjectByType<PowerUpTextManager>();
            if (textManager != null)
            {
                textManager.ShowPowerUpText("SHIELD!");
            }

            Destroy(gameObject);
        }
    }
}
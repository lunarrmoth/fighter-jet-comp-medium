using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Enemy2 : MonoBehaviour

{

    public GameObject explosionPrefab;

    private Vector3 startPosition;
    private GameManager gameManager;

    [Header("Square Patrol")]
    [SerializeField] private float sideLength = 4.0f;    // full side length of the square path
    [SerializeField] private float speed = 2.0f;         // movement speed (units/sec)
    [SerializeField] private float waitAtCorner = 0.0f;  // seconds to pause at each corner

    private Vector3[] corners = new Vector3[4];
    private int currentCorner = 0;
    private float waitTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Enemy";
        startPosition = transform.position;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        float half = sideLength * 0.5f;
        corners[0] = startPosition + new Vector3(-half, -half, 0f); // bottom-left
        corners[1] = startPosition + new Vector3(-half, half, 0f); // top-left
        corners[2] = startPosition + new Vector3(half, half, 0f); // top-right
        corners[3] = startPosition + new Vector3(half, -half, 0f); // bottom-right




    }

    // Update is called once per frame
    void Update()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        Vector3 target = corners[currentCorner];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            currentCorner = (currentCorner + 1) % corners.Length;
            waitTimer = waitAtCorner;
        }
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

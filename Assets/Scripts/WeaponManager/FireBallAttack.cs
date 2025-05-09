using UnityEngine;

public class FireBallAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject knight;
    private Rigidbody2D rb;
    private GameObject player;
    private Vector2 direction;
    private EnemyManager _enemyManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.position - transform.position;
    }

    public void SetEnemyManager(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(knight, transform.position, Quaternion.identity, _enemyManager.transform);
        }

        Destroy(gameObject);
    }
}
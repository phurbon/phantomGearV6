using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float stoppingDistance = 8f;
    public float shootingDistance = 6f;
    public float patrolDistance = 3f;
    public float patrolSpeed = 1f;

    private Rigidbody2D rb;
    private EnemyShooting enemyShooting;
    private Vector2 startingPosition;
    private bool isMovingRight = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyShooting = GetComponent<EnemyShooting>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        startingPosition = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        rb.WakeUp(); // Ensure movement isn't frozen

        if (distance > stoppingDistance)
        {
            // Patrolling behavior if player is far away
            Patrol();
        }
        else
        {
            // Chase the player if within range
            rb.linearVelocity = direction * speed;
        }

        // Shooting logic
        if (distance <= shootingDistance && enemyShooting != null)
        {
            enemyShooting.Shoot();
        }
    }

    void Patrol()
    {
        float patrolEdgeRight = startingPosition.x + patrolDistance;
        float patrolEdgeLeft = startingPosition.x - patrolDistance;

        if (isMovingRight)
        {
            rb.linearVelocity = new Vector2(patrolSpeed, 0);
            if (transform.position.x >= patrolEdgeRight)
            {
                isMovingRight = false;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-patrolSpeed, 0);
            if (transform.position.x <= patrolEdgeLeft)
            {
                isMovingRight = true;
            }
        }
    }
}

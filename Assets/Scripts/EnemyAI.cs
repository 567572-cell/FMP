using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Movement")]
    public float chaseRange = 10f;
    public float stoppingDistance = 1.5f;

    [Header("Attack")]
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 20;

    private NavMeshAgent agent;
    private Animator animator;

    private float lastAttackTime;

    private PlayerHealth playerHealth;

    public bool isAttacking;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (agent != null)
        {
            agent.stoppingDistance = stoppingDistance;
        }
    }

    void Start()
    {
        // Find player automatically
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null)
        {
            player = playerHealth.transform;
        }
    }

    void Update()
    {
        if (player == null || agent == null) return;

        float distanceToPlayer =
            Vector3.Distance(transform.position, player.position);

        HandleMovement(distanceToPlayer);

        HandleAnimation();

        HandleAttack(distanceToPlayer);
    }

    void HandleMovement(float distance)
    {
        // Stop while attacking
        if (isAttacking)
        {
            agent.ResetPath();
            return;
        }

        // Chase player
        if (distance <= chaseRange)
        {
            if (distance > attackRange)
            {
                if (agent.isOnNavMesh)
                {
                    agent.SetDestination(player.position);
                }
            }
            else
            {
                agent.ResetPath();
            }
        }
        else
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
            }
        }
    }

    void HandleAnimation()
    {
        if (animator == null) return;

        bool isMoving =
            agent.hasPath &&
            agent.remainingDistance > agent.stoppingDistance;

        animator.SetBool("IsWalking", isMoving);
    }

    void HandleAttack(float distance)
{
    if (distance <= attackRange &&
        Time.time >= lastAttackTime + attackCooldown)
    {
        lastAttackTime = Time.time;

        isAttacking = true;

        agent.ResetPath();

        transform.LookAt(player);

        // Play attack animation
        animator.SetTrigger("Attack");
        

        

        // DAMAGE PLAYER HERE
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);

            


        }

        // Allow movement again after short delay
        Invoke(nameof(EndAttack), 1f);
    }
}

    
    // Called at end of attack animation
    public void EndAttack()
    {
        isAttacking = false;
    }
}
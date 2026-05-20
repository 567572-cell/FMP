using UnityEngine;

public class StairDetector : MonoBehaviour
{
    public Transform groundCheck;
    public float rayDistance = 1.2f;

    [Header("Stair Settings")]
    public float minStairAngle = 25f;
    public float maxStairAngle = 60f;

    public bool isOnStairs;

  private Animator animator;

  private EnemyAI  enemyAI;
  public PlayerHealth playerHealth;

    void Start()
    {
        animator = GetComponent<Animator>();

        enemyAI = FindObjectOfType<EnemyAI>();

        playerHealth = FindObjectOfType<PlayerHealth>();
    }


    void Update()
    {
        if (playerHealth.isAttackedPlaying != true)
        {
               DetectStairs();
        }
        
     
    }

    void DetectStairs()
    {
        RaycastHit hit;

        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, rayDistance))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);



            // Detect "stair-like" surfaces
            if (angle > minStairAngle && angle < maxStairAngle)
            {
                isOnStairs = true;

                animator.SetBool("IsClimbing", isOnStairs);
                Debug.Log("On Stairs");
            }
            else
            {
                isOnStairs = false;
                //animator.SetBool("IsClimbing", isOnStairs);
            }
        }
    }
}
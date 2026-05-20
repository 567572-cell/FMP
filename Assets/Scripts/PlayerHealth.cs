using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    private Animator animator;

    // Prevents the attack animation from restarting repeatedly
    public bool isAttackedPlaying = false;

    // How long the attacked state lasts
    [SerializeField] private float attackedDuration = 1f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        //Debug.Log("Player Health: " + health);

        // Only start the coroutine if it's not already running
        if (!isAttackedPlaying)
        {
            StartCoroutine(PlayAttackedAnimation());
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator PlayAttackedAnimation()
    {
        isAttackedPlaying = true;

        animator.SetBool("IsAttacked", true);

        // Wait for animation duration
        yield return new WaitForSeconds(attackedDuration);

        animator.SetBool("IsAttacked", false);

        isAttackedPlaying = false;
    }

    void Die()
    {
        Debug.Log("Game Over");

        // Disable player
        gameObject.SetActive(false);

        // You can later add:
        // restart scene
        // game over screen
    }
}
using System.Collections;
using UnityEngine;

// Funciones:
// - Patrullar
// - Detectar jugador
// - Perseguir jugador
// - Atacar cuando esté cerca

public class EnemyAttack : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    private Rigidbody2D enemyRb;

    [Header("Movimiento")]
    public float moveSpeed = 2f;

    [Header("Detección")]
    public float detectionRange = 6f;

    [Header("Ataque")]
    public float attackRange = 1.2f;
    public int damage = 1;
    public float attackCooldown = 2f;

    private bool canAttack = true;
    Powers powers;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();

        powers = GetComponent<Powers>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            FollowPlayer();
        }

        if (distance <= attackRange && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        enemyRb.linearVelocity = new Vector2(direction.x * moveSpeed, enemyRb.linearVelocity.y);
    }

    IEnumerator Attack()
    {
        canAttack = false;

        Debug.Log("Enemigo atacó al jugador");

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
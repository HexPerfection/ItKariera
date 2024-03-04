using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    public enum BossStage { First, Second, Third }

    public float moveSpeed = 5f;
    public Transform target; // Player's transform
    public GameObject bulletPrefab;
    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public Transform firePoint;
    public Transform[] spawnPoints; // Points where enemies will be spawned
    public Transform attackPoint; // Point where the attack originates
    public LayerMask playerLayer; // Layer mask for detecting the player

    public float meleeAttackCooldown = 2f;
    public float rangeAttackCooldown = 2f;
    public float spawnCooldown = 10f;
    public int damageIncrease = 10;

    private float nextMeleeAttackTime = 0f;
    private float nextRangeAttackTime = 0f;
    private float nextSpawnTime = 0f;
    private BossStage currentStage = BossStage.First;

    //private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Move towards the player
        Vector2 targetDirection = target.position - transform.position;
        rb.velocity = targetDirection.normalized * moveSpeed;


        // Update boss behavior based on current stage
        switch (currentStage)
        {
            case BossStage.First:
                NormalAttack();
                break;
            case BossStage.Second:
                Debug.Log("Second stage");
                SpawnEnemiesEvery10Seconds();
                NormalAttack();
                break;
            case BossStage.Third:
                QuickMove();
                SpawnEnemiesEvery10Seconds();
                NormalAttack();
                ShootAtPlayer();
                break;
        }
    }

    private void NormalAttack()
    {
        if (Time.time >= nextMeleeAttackTime)
        {
            // Attack player
            Attack();
            nextMeleeAttackTime = Time.time + meleeAttackCooldown;
        }
    }

    private void SpawnEnemiesEvery10Seconds()
    {
        if (Time.time >= nextSpawnTime)
        {
            // Spawn enemies
            SpawnEnemies();
            nextSpawnTime = Time.time + spawnCooldown;
        }
    }

    private void QuickMove()
    {
        // Increase movement speed
        //moveSpeed = 7.5f;
    }

    private void Attack()
    {
        // Play attack animation
        //animator.SetTrigger("Attack");

        // Detect player
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, 1f, playerLayer);
        // Damage player if hit
        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<PlayerHealth>().DamagePlayer(10 + (int)currentStage * damageIncrease); // Adjust damage as needed
        }
    }

    private void ShootAtPlayer()
    {
        if (Time.time >= nextRangeAttackTime)
        {
            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // Set bullet direction towards the player
            bullet.GetComponent<Rigidbody2D>().velocity = (target.position - firePoint.position).normalized * 10f;
            // Set next attack time
            nextRangeAttackTime = Time.time + rangeAttackCooldown;
        }
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    // Draw a gizmo for the attack point
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, 1f);
    }

    // Method to set boss stage
    public void SetBossStage(BossStage stage)
    {
        currentStage = stage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMovement : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    public float moveSpeed = 3f; // Movement speed of the enemy
    public float stoppingDistance = 5f; // Distance at which the enemy stops moving towards the player
    public float retreatDistance = 3f; // Distance at which the enemy retreats from the player
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    public Transform firePoint; // Point from which projectiles are spawned
    public float fireRate = 2f; // Rate of fire in shots per second

    private float nextFireTime; // Time of the next allowed shot


    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }
    void Update()
    {
        // Move towards the player
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                // Stay at stopping distance
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                // Retreat from the player
                transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
            }

            // Rotate towards the player
            Vector2 direction = player.position - transform.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //firePoint.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            // Shoot at the player
            if (Time.time >= nextFireTime && Vector2.Distance(transform.position, player.position) <= stoppingDistance)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        // Spawn projectile at fire point
        float angle = Mathf.Atan2(firePoint.right.y, firePoint.right.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce((player.position - firePoint.position).normalized * 20, ForceMode2D.Impulse);

    }
}

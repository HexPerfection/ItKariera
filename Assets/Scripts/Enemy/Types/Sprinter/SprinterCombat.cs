using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinterCombat : MonoBehaviour
{

    public int damage = 10;
    public float knockbackForce = 10f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }

            // Calculate knockback direction
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // Apply knockback force to the player
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            
            rb.velocity = Vector2.zero; // Reset player's current velocity
            rb.velocity = knockbackDirection * knockbackForce;
        
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public GameObject collisionEffect;
    public int weaponId;

    private float slowDuration = 1f;
    private float slowFactor = 0.5f;
    
    void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (CompareTag("Player"))
        {
            if (collision2D.gameObject.tag == "Enemy" && collision2D.gameObject.GetComponent<Bullet>() == null)
            {
                if (collision2D.gameObject.GetComponent<BossHealth>() != null) 
                {
                    collision2D.gameObject.GetComponent<BossHealth>().TakeDamage(Mathf.CeilToInt(damage));
                } else
                {
                    collision2D.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                }

                if (weaponId == 4)
                {
                    if (collision2D.gameObject.GetComponent<SprinterMovement>() != null)
                    {
                        collision2D.gameObject.GetComponent<SprinterMovement>().SlowDown(slowDuration, slowFactor);
                    }
                    else
                    {
                        collision2D.gameObject.GetComponent<ShooterMovement>().SlowDown(slowDuration, slowFactor);
                    }
                }
                
            }

        } else if (CompareTag("Enemy"))
        {
            if (collision2D.gameObject.tag == "Player" && collision2D.gameObject.GetComponent<Bullet>() == null)
            {
                collision2D.gameObject.GetComponent<PlayerHealth>().DamagePlayer(((int)damage));
            }

            if (weaponId == 4)
            {
                collision2D.gameObject.GetComponent<PlayerMovement>().SlowDown(slowDuration, slowFactor);
            }
        }

        if (collisionEffect != null)
        {
            Instantiate(collisionEffect, transform.position, Quaternion.identity);
        }      
        Destroy(gameObject);     
    }
}

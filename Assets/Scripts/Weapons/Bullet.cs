using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    
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
                
            }

        } else if (CompareTag("Enemy"))
        {
            if (collision2D.gameObject.tag == "Player" && collision2D.gameObject.GetComponent<Bullet>() == null)
            {
                collision2D.gameObject.GetComponent<PlayerHealth>().DamagePlayer(((int)damage));
            }
        }
        Destroy(gameObject);
    }
}

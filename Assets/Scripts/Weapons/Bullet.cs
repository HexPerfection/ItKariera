using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1;
    
    void OnTriggerEnter2D(Collider2D collision2D)
    {

        if (collision2D.gameObject.tag == "Enemy")
        {
            collision2D.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}

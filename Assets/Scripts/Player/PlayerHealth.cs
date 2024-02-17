using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float damageRate = 1f;

    void Start()
    {
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DamagePlayer(10);

        }
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage * damageRate;

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }
}

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

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage * damageRate;

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }
}

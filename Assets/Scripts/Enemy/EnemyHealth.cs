using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string id;
    public float maxHealth = 3;
    public float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        id = System.Guid.NewGuid().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

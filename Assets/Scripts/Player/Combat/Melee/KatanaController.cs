using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : MonoBehaviour
{
    //Melee
    public Transform attackPoint;
    private float damage = 5;
    private float attackRange = 1f;
    public float attackRate = 2f;

    public LayerMask enemyLayer;
    private float nextAttack;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttack && transform.parent != null)
        {
            if (Input.GetButton("Fire1"))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
    }

    public void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
        }
    }
}

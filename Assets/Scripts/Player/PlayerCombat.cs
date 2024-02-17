using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Shooting
    
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public float rangedAttackRate = 2f;
    float nextRangedAttack = 0f;

    //Melee
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float meleeAttackRate = 2f;
    float nextMeleeAttack = 0f;

    public LayerMask enemyLayer;

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextRangedAttack)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
                nextRangedAttack = Time.time + 1f / rangedAttackRate;
            }
        }

        if (Time.time > nextMeleeAttack)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                Attack();
                nextMeleeAttack = Time.time + 1f / meleeAttackRate;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }

    void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyController : MonoBehaviour
{
    private Transform firePoint;
    public GameObject bulletPrefab;

    private float bulletForce = 25f;
    public float attackRate = 0.5f;

    private float damage = 10f;

    public LayerMask enemyLayer;
    private float nextAttack;

    private void Start()
    {
        firePoint = GameObject.FindGameObjectsWithTag("Player")[0].transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttack && transform.parent != null)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().damage = damage;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}

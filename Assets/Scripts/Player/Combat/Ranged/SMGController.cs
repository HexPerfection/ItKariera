using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGController : MonoBehaviour
{
    private Transform firePoint;
    public GameObject bulletPrefab;

    private float bulletForce = 20f;
    public float attackRate = 8f;

    private float damage = 0.5f;

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
        float angle = Mathf.Atan2(firePoint.right.y, firePoint.right.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));
        bullet.GetComponent<Bullet>().damage = damage;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}

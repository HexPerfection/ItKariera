using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SaveEnemies : MonoBehaviour, ISaveLoad
{
    public GameObject[] enemyTemplates;


    public void LoadData(GameData data)
    {
        GameObject[] oldEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in oldEnemies)
        {
            Destroy(enemy);
        }

        List<Enemy> enemies = data.enemyData.enemies;

        foreach (Enemy enemy in enemies)
        {
            GameObject currentEnemy;

            if (enemy.shooterMoveSpeed != 0)
            {
                if (enemy.shooterFireRate > 1)
                {
                    currentEnemy = enemyTemplates[2];
                }
                else
                {
                    currentEnemy = enemyTemplates[3];
                }

                ShooterMovement shooterMovement = currentEnemy.GetComponent<ShooterMovement>();
                shooterMovement.moveSpeed = enemy.shooterMoveSpeed;
                shooterMovement.fireRate = enemy.shooterFireRate;
                shooterMovement.retreatDistance = enemy.shooterRetreatDistance;
                shooterMovement.stoppingDistance = enemy.shooterStoppingDistance;
            }
            else
            {
                if (enemy.sprinterMoveSpeed > 3)
                {
                    currentEnemy = enemyTemplates[0];
                } else
                {
                    currentEnemy = enemyTemplates[1];
                }

                SprinterMovement sprinterMovement = currentEnemy.AddComponent<SprinterMovement>();
                SprinterCombat sprinterCombat = currentEnemy.AddComponent<SprinterCombat>();

                sprinterMovement.speed = enemy.sprinterMoveSpeed;
                sprinterCombat.damage = enemy.sprinterDamage;
                sprinterCombat.knockbackForce = enemy.sprinterKnockbackForce;
            }

            EnemyHealth enemyHealth = currentEnemy.GetComponent<EnemyHealth>();
            enemyHealth.id = enemy.id;
            enemyHealth.maxHealth = enemy.maxHealth;
            enemyHealth.currentHealth = enemy.currentHealth;

            currentEnemy.transform.position = enemy.position;

            Instantiate(currentEnemy, enemy.position, Quaternion.identity);
        }
    }

    public void SaveData(GameData data)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            SprinterMovement sprinterMovement = enemy.GetComponent<SprinterMovement>();
            SprinterCombat sprinterCombat = enemy.GetComponent<SprinterCombat>();
            ShooterMovement shooterMovement = enemy.GetComponent<ShooterMovement>();
            
            Enemy currentEnemy = new Enemy();
            currentEnemy.position = enemy.transform.position;

            currentEnemy.id = enemyHealth.id;
            currentEnemy.maxHealth = enemyHealth.maxHealth;
            currentEnemy.currentHealth = enemyHealth.currentHealth;

            if (shooterMovement != null)
            {
                currentEnemy.shooterMoveSpeed = shooterMovement.moveSpeed;
                currentEnemy.shooterFireRate = shooterMovement.fireRate;
                currentEnemy.shooterRetreatDistance = shooterMovement.retreatDistance;
                currentEnemy.shooterStoppingDistance = shooterMovement.stoppingDistance;
            } else
            {
                currentEnemy.sprinterMoveSpeed = sprinterMovement.speed;
                currentEnemy.sprinterDamage = sprinterCombat.damage;
                currentEnemy.sprinterKnockbackForce = sprinterCombat.knockbackForce;
            }

            data.enemyData.enemies.Add(currentEnemy);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

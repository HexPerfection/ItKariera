using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SaveEnemies : MonoBehaviour, ISaveLoad
{
    public GameObject[] enemyPrefabs;


    public void LoadData(GameData data)
    {
        //Delete old boss
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));

        List<Enemy> enemies = data.enemyData.enemies;

        foreach (Enemy enemy in enemies)
        {
            GameObject currentEnemy = Instantiate(enemyPrefabs[enemy.id], enemy.position, Quaternion.identity);

            if (enemy.id == 4)
            {
                BossHealth bossHealth = currentEnemy.GetComponent<BossHealth>();
                bossHealth.currentHealth = (int)enemy.currentHealth;

                //Force to see if stage should change
                bossHealth.TakeDamage(0);
            } else
            {
                EnemyHealth enemyHealth = currentEnemy.GetComponent<EnemyHealth>();
                enemyHealth.currentHealth = enemy.currentHealth;
            }
            
            
        }
    }

    public void SaveData(GameData data)
    {
        data.enemyData.enemies.Clear();
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Enemy currentEnemy = new Enemy();

            if (enemy.GetComponent<Bullet>() != null)
            {
                return;
            }

            if (enemy.GetComponent<BossHealth>() != null)
            {
                currentEnemy.id = enemy.GetComponent<BossHealth>().id;
                currentEnemy.currentHealth = enemy.GetComponent<BossHealth>().currentHealth;
            } else
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                Debug.Log(enemyHealth);
                currentEnemy.id = enemyHealth.id;
                currentEnemy.currentHealth = enemyHealth.currentHealth;
            }

            currentEnemy.position = enemy.transform.position;
            data.enemyData.enemies.Add(currentEnemy);

        }
    }
}

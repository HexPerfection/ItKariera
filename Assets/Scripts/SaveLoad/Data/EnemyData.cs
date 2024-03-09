using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public List<Enemy> enemies;

    public EnemyData()
    {
        enemies = new List<Enemy>();
    }

}

[System.Serializable]
public class Enemy
{

    public string id;
    public Vector2 position = new Vector2(100, 100);
    
    public float maxHealth;
    public float currentHealth;

    public float shooterMoveSpeed;
    public float shooterStoppingDistance;
    public float shooterRetreatDistance;
    public float shooterFireRate;

    public float sprinterMoveSpeed;
    public int sprinterDamage;
    public float sprinterKnockbackForce;

}

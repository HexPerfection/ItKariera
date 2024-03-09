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
    public int id = 0;
    public Vector2 position = Vector2.zero;
    
    public float currentHealth = 10;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ISaveLoad
{
    public float currentHealth;
    public float maxHealth = 100;
    public float damageRate = 1f;

    public GameObject player;
    public HealthBar healthBar;
    public Canvas deathMenu;

    void Start()
    {
    }

    void Update()
    {

    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage * damageRate;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            //Destroy(player);
            Time.timeScale = 0;
            deathMenu.enabled = true;
        }
    }

    public void LoadData(GameData data)
    {
        currentHealth = data.playerAttributesData.currentHealth;
        maxHealth = data.playerAttributesData.maxHealth;
        damageRate = data.playerAttributesData.damageRate;
    }

    public void SaveData(GameData data)
    {
        data.playerAttributesData.currentHealth = currentHealth;
        data.playerAttributesData.maxHealth = maxHealth;
        data.playerAttributesData.damageRate = damageRate;
    }
}

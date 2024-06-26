using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int scoreGain = 300;
   // public Slider healthSlider; // Reference to the UI slider for boss health
    public BossCombat bossCombat; // Reference to the boss controller script

    public int currentHealth;
    public int id = 5;

    public GameObject deathEffect;

    private void Start()
    {
        currentHealth = maxHealth;
        //UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //UpdateHealthUI();

        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else if ((currentHealth <= maxHealth / 2 && currentHealth > maxHealth / 3) && bossCombat != null)
        {
            bossCombat.SetBossStage(BossCombat.BossStage.Second);
        } else if (currentHealth <= maxHealth / 3 && bossCombat != null)
        {
            Debug.Log("Third Stage");
            bossCombat.SetBossStage(BossCombat.BossStage.Third);
        }
    }

    private void Die()
    {
        // Handle boss death (e.g., play death animation, spawn loot, etc.)
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        FindAnyObjectByType<PlayerScore>().AddScore(scoreGain);
        Destroy(gameObject);
        Debug.Log("Boss died. Victory!");
    }

    /*private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }*/
}

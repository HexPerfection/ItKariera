using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        healthBar.fillAmount = 1;
    }

    public void SetHealth(float hp)
    {
        float fillPercent = hp / playerHealth.maxHealth;
        
        healthBar.fillAmount = fillPercent;
    }
}

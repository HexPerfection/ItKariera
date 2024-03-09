using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerup : MonoBehaviour
{
    [SerializeField]
    private int _healMultiplier = 20;
    [SerializeField]
    private float _powerupDuration = 5;

    [SerializeField]
    private GameObject _art;
    private Collider2D _collider;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            StartCoroutine(PowerupSequence(playerHealth));
        }
    }

    public IEnumerator PowerupSequence(PlayerHealth playerHealth)
    {
        _collider.enabled = false;
        _art.SetActive(false);

        ActivatePowerup(playerHealth);

        yield return new WaitForSeconds(_powerupDuration);

        DeactivatePowerup(playerHealth);

        Destroy(gameObject);
    }

    private void ActivatePowerup(PlayerHealth playerHealth)
    {
        playerHealth.currentHealth = Mathf.Clamp(_healMultiplier + playerHealth.currentHealth, 0, 100);
        playerHealth.healthBar.SetHealth(playerHealth.currentHealth);
    }

    private void DeactivatePowerup(PlayerHealth playerHealth)
    {
        // Implement a feature such as taking less damage while powerup is active
        
        Debug.Log("Powerup Ended");
    }
}

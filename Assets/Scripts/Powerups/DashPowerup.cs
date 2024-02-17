using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : MonoBehaviour
{
    [SerializeField]
    private float _dashPowerMultiplier = 2;
    [SerializeField]
    private float _dashCooldown = 0.5f;
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
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            StartCoroutine(PowerupSequence(playerMovement));
        }
    }

    public IEnumerator PowerupSequence(PlayerMovement playerMovement)
    {
        _collider.enabled = false;
        _art.SetActive(false);

        ActivatePowerup(playerMovement);

        yield return new WaitForSeconds(_powerupDuration);

        DeactivatePowerup(playerMovement);

        Destroy(gameObject);
    }

    private void ActivatePowerup(PlayerMovement playerMovement)
    {
        playerMovement.dashPower *= _dashPowerMultiplier;
        playerMovement.dashCooldown = _dashCooldown;
    }

    private void DeactivatePowerup(PlayerMovement playerMovement)
    {
        playerMovement.dashPower = 10;
        playerMovement.dashCooldown = 3f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerup : MonoBehaviour
{
    [SerializeField]
    private int _damageMultiplier = 2;
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
        PlayerCombat playerCombat = other.GetComponent<PlayerCombat>();

        if (playerCombat != null)
        {
            StartCoroutine(PowerupSequence(playerCombat));
        }
    }

    public IEnumerator PowerupSequence(PlayerCombat playerCombat)
    {
        _collider.enabled = false;
        _art.SetActive(false);

        ActivatePowerup(playerCombat);

        yield return new WaitForSeconds(_powerupDuration);

        DeactivatePowerup(playerCombat);

        Destroy(gameObject);
    }

    private void ActivatePowerup(PlayerCombat playerCombat)
    {
        playerCombat.damageMultiplier = _damageMultiplier;
    }

    private void DeactivatePowerup(PlayerCombat playerCombat)
    {
        playerCombat.damageMultiplier = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributesData
{
    public float speedMultiplier = 1;

    public float dashPower = 10f;
    public float dashLength = 0.2f;
    public float dashCooldown = 3;

    public float currentHealth = 100;
    public float maxHealth = 100;
    public float damageRate = 1;

    public int damageMultiplier = 1;

    public bool hasWeapon = false;
    public int currentWeaponId = 0;

}

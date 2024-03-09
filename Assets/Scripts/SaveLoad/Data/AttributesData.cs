using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributesData
{
    public float speedMultiplier = 1;

    public float dashPower = 25f;
    public float dashLength = 2f;
    public float dashCooldown = 2;

    public float currentHealth = 100;
    public float maxHealth = 100;
    public float damageRate = 1;

    public int damageMultiplier = 1;

    public GameObject currentWeapon = null;

}

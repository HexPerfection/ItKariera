using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public List<SingleWeapon> weapons;

    public WeaponData()
    {
        weapons = new List<SingleWeapon>();
    }

}

[System.Serializable]
public class SingleWeapon
{
    public int weaponId;
    public Vector2 position = Vector2.zero;
}

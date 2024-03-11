using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWeapons : MonoBehaviour, ISaveLoad
{
    public GameObject[] weaponPrefabs;

    public void LoadData(GameData data)
    {
        //Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        
        List<SingleWeapon> weapons = data.weaponData.weapons;

        foreach (SingleWeapon weapon in weapons)
        {
            Instantiate(weaponPrefabs[weapon.weaponId], weapon.position, Quaternion.identity);
        }
    }

    public void SaveData(GameData data)
    {
        data.weaponData.weapons.Clear();

        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");

        foreach (GameObject weapon in weapons)
        {
           if (weapon.transform.parent == null)
           {
                Weapon weaponScript = weapon.GetComponent<Weapon>();

                SingleWeapon currentWeapon = new SingleWeapon
                {
                    weaponId = weaponScript.id,
                    position = weapon.transform.position
                };

                data.weaponData.weapons.Add(currentWeapon);
           }      
        }
    }
}

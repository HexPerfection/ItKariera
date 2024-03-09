using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePowerups : MonoBehaviour, ISaveLoad
{
    public GameObject[] powerupPrefabs;

    public void LoadData(GameData data)
    {
        List<SinglePowerup> powerups = data.powerupData.powerups;

        foreach (SinglePowerup powerup in powerups)
        {
            Instantiate(powerupPrefabs[powerup.powerupId], powerup.position, Quaternion.identity);
        }
    }

    public void SaveData(GameData data)
    {
        data.powerupData.powerups.Clear();

        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");

        foreach (GameObject powerup in powerups)
        {
            Powerup powerupScript = powerup.GetComponent<Powerup>();

            SinglePowerup currentPowerup = new SinglePowerup
            {
                powerupId = powerupScript.id,
                position = powerup.transform.position
            };
            
            data.powerupData.powerups.Add(currentPowerup);

        }
    }

}

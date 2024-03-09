using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerupData
{
    public List<SinglePowerup> powerups;

    public PowerupData()
    {
        powerups = new List<SinglePowerup>();
    }

}

[System.Serializable]
public class SinglePowerup
{
    public int powerupId = 0;
    public Vector2 position = Vector2.zero;
}

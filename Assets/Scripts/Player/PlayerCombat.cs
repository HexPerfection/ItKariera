using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, ISaveLoad
{

    public int damageMultiplier = 1;

    public void LoadData(GameData data)
    {
        damageMultiplier = data.playerAttributesData.damageMultiplier;
    }

    public void SaveData(GameData data)
    {
        data.playerAttributesData.damageMultiplier = damageMultiplier;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
    }
}

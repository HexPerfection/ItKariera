using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector2 playerPosition = new Vector2(15, -108);
    
    public AttributesData playerAttributesData = new AttributesData();
    public EnemyData enemyData = new EnemyData();
    public RoomData roomData = new RoomData();
    public BoxData boxData = new BoxData();
    public PowerupData powerupData = new PowerupData();
    public WeaponData weaponData = new WeaponData();
    
}
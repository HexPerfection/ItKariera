using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector2 playerPosition = new Vector2(100, 100);
    
    public AttributesData playerAttributesData = new AttributesData();
    public EnemyData enemyData;
    public RoomData roomData;
    
}
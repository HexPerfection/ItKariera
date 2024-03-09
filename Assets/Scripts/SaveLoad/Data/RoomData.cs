using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomData
{
    public List<SingleRoom> rooms;

    public RoomData()
    {
        rooms = new List<SingleRoom>();
    }
}

[System.Serializable]
public class SingleRoom
{
    public int roomType = 0;
    public int roomIndex = 0;

    public Vector2 position = Vector2.zero;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomType;
    public int roomIndex;

    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}

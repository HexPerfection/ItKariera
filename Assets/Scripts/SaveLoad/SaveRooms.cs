using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRooms : MonoBehaviour, ISaveLoad
{

    public GameObject[] roomPrefabs;
    
    public void LoadData(GameData data)
    {
        List<SingleRoom> rooms = data.roomData.rooms;

        foreach (SingleRoom room in rooms)
        {
            int roomIndex = room.roomIndex;

            foreach (GameObject roomPrefab in roomPrefabs)
            {
                Room roomScript = roomPrefab.GetComponent<Room>(); 
                
                if (roomIndex == roomScript.roomIndex)
                {
                    Instantiate(roomPrefab, room.position, Quaternion.identity);
                }
            }
        }
    }

    public void SaveData(GameData data)
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

        foreach (GameObject room in rooms) 
        {
            
            Room roomScript = room.GetComponent<Room>();

            SingleRoom currentRoom = new SingleRoom
            {
                position = room.transform.position,
                roomIndex = roomScript.roomIndex,
                roomType = roomScript.roomType
            };

            data.roomData.rooms.Add(currentRoom);

        }
    }

}

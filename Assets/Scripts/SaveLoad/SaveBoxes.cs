using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBoxes : MonoBehaviour, ISaveLoad
{
    public GameObject[] boxPrefabs;

    public void LoadData(GameData data)
    {
        List<SingleBox> boxes = data.boxData.boxes;

        foreach (SingleBox box in boxes)
        {
            GameObject currentBox = Instantiate(boxPrefabs[box.boxId], box.position, Quaternion.identity);

            Box boxScript = currentBox.GetComponent<Box>();
            boxScript.health = box.health;
        }
    }

    public void SaveData(GameData data)
    {
        data.boxData.boxes.Clear();

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject box in boxes)
        {
            Box boxScript = box.GetComponent<Box>();

            SingleBox currentBox = new SingleBox
            {
                boxId = boxScript.id,
                health = boxScript.health,
                position = boxScript.transform.position
            };

            data.boxData.boxes.Add(currentBox);

        }
    }
}

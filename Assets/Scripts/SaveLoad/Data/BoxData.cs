using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoxData
{
    public List<SingleBox> boxes;

    public BoxData()
    {
        boxes = new List<SingleBox>();
    }

}

[System.Serializable]
public class SingleBox
{
    public int boxId = 0;
    public float health = 10;
    public Vector2 position = Vector2.zero;
}

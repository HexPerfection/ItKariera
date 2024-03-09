using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector2 offset;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -5); // Camera follows the player with specified offset position
    }
}

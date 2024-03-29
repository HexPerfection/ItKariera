using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour, ISaveLoad
{

    public Transform[] startingPositions;
    public GameObject[] rooms; // index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRBT

    public GameObject bossRoom;

    private int direction;
    public bool stopGeneration;
    private int downCounter;

    public float moveIncrement;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    public LayerMask whatIsRoom;

    public int maxX = 60;
    public int maxY = -60;

    public bool shouldGenerate = true;


    private void Start()
    {
        Debug.Log("Should genereate:" + shouldGenerate);
        
        if (shouldGenerate && !LoadSettings.shouldLoadFile)
        {
            int randStartingPos = Random.Range(0, startingPositions.Length);
            transform.position = startingPositions[randStartingPos].position;
            Instantiate(rooms[1], transform.position, Quaternion.identity);
            direction = Random.Range(1, 6);
        }

        Instantiate(bossRoom, new Vector2(40, -100), Quaternion.identity);

    }

    private void Update()
    {

        if (shouldGenerate)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (timeBtwSpawn <= 0 && stopGeneration == false)
            {
                Move();
                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
    }

    private void Move()
    {

        if (direction == 1 || direction == 2)
        { // Move right !

            if (transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(0, rooms.Length);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                // Makes sure the level generator doesn't move left
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        { // Move left !

            if (transform.position.x > 0)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(0, rooms.Length);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }

        }
        else if (direction == 5)
        { // MoveDown

            downCounter++;
            if (transform.position.y > maxY)
            {
                // Now I must replace the room BEFORE going down with a room that has a DOWN opening, so type 3 or 5
                Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);

                if (previousRoom.GetComponent<Room>().roomType != 1 && previousRoom.GetComponent<Room>().roomType != 3)
                {
                    if (downCounter >= 2)
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        int randRoomDownOpening = Random.Range(1, 4);
                        if (randRoomDownOpening == 2)
                        {
                            randRoomDownOpening = 1;
                        }
                        Instantiate(rooms[randRoomDownOpening], transform.position, Quaternion.identity);
                    }

                }

                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = pos;

                // Makes sure the room we drop into has a TOP opening !
                int randRoom = Random.Range(2, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
            }

        }
    }

    public void LoadData(GameData data)
    {
        if (data.roomData.rooms.Count != 0)
        {
            shouldGenerate = false;
        }
    }

    public void SaveData(GameData data)
    {
        Debug.Log("Saved");
    }
}
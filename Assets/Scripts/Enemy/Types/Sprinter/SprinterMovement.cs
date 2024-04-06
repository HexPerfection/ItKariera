using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinterMovement : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private bool isSlowed = false;
    
    public SpriteRenderer sr;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];  
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < 10)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }  
        
    }

    public void SlowDown(float slowDuration, float slowFactor)
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowDownCoroutine(slowDuration, slowFactor));
        }
        
    }

    private IEnumerator SlowDownCoroutine(float duration, float factor)
    {
        float originalSpeed = speed;
        speed *= factor; // Slow down the enemy
        isSlowed = true;
        sr.color = Color.cyan;

        yield return new WaitForSeconds(duration); // Wait for the specified duration

        sr.color = Color.white;
        speed = originalSpeed; // Restore the original speed
        isSlowed = false;
    }
}

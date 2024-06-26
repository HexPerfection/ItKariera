using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject[] lootItems; // Array of weapons or powerups to drop
    public float destroyDelay = 0.5f; // Delay before destroying the loot box
    public float health = 5;
    public int id;
    public int scoreGain = 10;


    public void DestroyLootBox()
    {
        // Trigger any animations or effects here

        GetComponent<Collider2D>().enabled = false;

        FindAnyObjectByType<PlayerScore>().AddScore(scoreGain);

        int randomIndex = Random.Range(0, lootItems.Length);
        GameObject randomLoot = lootItems[randomIndex];
        Instantiate(randomLoot, transform.position, Quaternion.identity);

        Destroy(gameObject, destroyDelay);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            health -= collision.gameObject.GetComponent<Bullet>().damage;

            if (health <= 0)
            {
                DestroyLootBox();
            }
        }
    }
}
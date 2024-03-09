using UnityEngine;
using System.Collections.Generic;

public class PlayerPickup : MonoBehaviour, ISaveLoad
{
    private GameObject currentWeapon; // Track the current weapon
    public Transform player; // Where the weapon should be attached on the player

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentWeapon != null)
        {
            DropCurrentWeapon();
        } else if (Input.GetKeyDown(KeyCode.E) && currentWeapon == null)
        {
            PickUpWeapon();
        }
    }

    /*void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.CompareTag("Weapon") && Input.GetKeyDown(KeyCode.E) && currentWeapon == null)
        {
            PickUpWeapon();
        }
    }*/

    // Function to pick up a weapon
    void PickUpWeapon()
    {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f); // Change 1f to the desired pick-up radius
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Weapon"))
                {
                    GameObject weapon = collider.gameObject;
                    currentWeapon = weapon;

                    // Attach weapon to player
                    weapon.transform.parent = player;
                    weapon.transform.position = player.transform.position;
                    weapon.transform.rotation = player.rotation;

                    // Disable weapon's collider and renderer
                    weapon.GetComponent<Collider2D>().enabled = false;
                    break; // Stop after picking up one weapon
                }
            }
    }

    // Function to drop the current weapon
    public void DropCurrentWeapon()
    {
            // Detach weapon from player
        currentWeapon.transform.parent = null;

            // Reset weapon's position (optional)
        currentWeapon.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);

            // Enable weapon's collider and renderer
        currentWeapon.GetComponent<Collider2D>().enabled = true;

        currentWeapon = null; // Remove reference to the dropped weapon
    }

    public void LoadData(GameData data)
    {
        currentWeapon = data.playerAttributesData.currentWeapon;
    }

    public void SaveData(GameData data)
    {
        data.playerAttributesData.currentWeapon = currentWeapon;
    }
}
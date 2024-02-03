using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 MovementSpeed; // 2D Movement speed to have independant axis speed
    public float speedMultiplier = 1;
    private Rigidbody2D rb; // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    private Vector2 inputVector = new Vector2(0.0f, 0.0f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDrag = 0.0f;
        rb.gravityScale = 0.0f;
    }

    void Update()
    {
        // Gives a value between -1 and 1
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + (inputVector * MovementSpeed * speedMultiplier * Time.fixedDeltaTime));

        // Rotation
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 perpendicular = Vector3.Cross(transform.position - mousePos, Vector3.forward);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }

}
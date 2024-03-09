using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ISaveLoad
{
    public Vector2 MovementSpeed; // 2D Movement speed to have independant axis speed
    public float speedMultiplier = 1;
    private Rigidbody2D rb; // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    private Vector2 inputVector = new Vector2(0.0f, 0.0f);
    public Transform firePoint;

    public float dashPower = 25f;
    public float dashLength = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDash = false;

    public DashBar dashBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDrag = 0.0f;
        rb.gravityScale = 0.0f;
    }

    void Update()
    {
        if (isDash)
        {
            return;
        }
        // Gives a value between -1 and 1
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if (canDash) 
            {
                StartCoroutine(Dash());
            }    
        }
    }

    void FixedUpdate()
    {
        if (isDash)
        {
            return;
        }
        // Movement
        rb.MovePosition(rb.position + (inputVector * MovementSpeed * speedMultiplier * Time.fixedDeltaTime));

        // Rotation
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 perpendicular = Vector3.Cross(transform.position - mousePos, Vector3.forward);
        firePoint.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDash = true;
        dashBar.EmptyBar();
        rb.velocity = inputVector * dashPower * speedMultiplier;
        yield return new WaitForSeconds(dashLength);
        //Add animation
        dashBar.FillBar();
        isDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerPosition;
        speedMultiplier = data.playerAttributesData.speedMultiplier;
        dashPower = data.playerAttributesData.dashPower;
        dashLength = data.playerAttributesData.dashLength;
        dashCooldown = data.playerAttributesData.dashCooldown;
        

    }

    public void SaveData(GameData data)
    {
        data.playerPosition = transform.position;
        data.playerAttributesData.speedMultiplier = speedMultiplier;
        data.playerAttributesData.dashPower = dashPower;
        data.playerAttributesData.dashLength = dashLength;
        data.playerAttributesData.dashCooldown = dashCooldown;
    }
}
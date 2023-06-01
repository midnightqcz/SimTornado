using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    // The base speed of the tornado.
    public float tornadoSpeed = 2f;

    // The multiplier applied to the tornado's speed when the left shift key is pressed.
    public float speedMultiplier = 1.5f;

    // The force with which the tornado jumps.
    public float jumpForce = 10f;

    // The transform used to check if the tornado is grounded.
    public Transform GroundCheck;

    // The distance from the ground check transform within which the tornado is considered to be grounded.
    public float GroundDistance = 0.5f;

    // The layer mask used to identify the ground layer.
    public LayerMask GroundMask;

    // indicating whether the tornado is currently grounded or not.
    public bool isGrounded;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    // This function controls the movement of the tornado based on user input.
    void tornadoMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        // Create a vector representing the movement direction.
        Vector3 movingDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movingDirection != Vector3.zero)
        {
            // Rotate the tornado to face the movement direction
            transform.rotation = Quaternion.LookRotation(-movingDirection);
        }

        // If the left shift key is pressed...
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // set the tornado's velocity to be its base speed multiplied by the speed multiplier, in the direction of the movement input.
            rb.velocity = new Vector3(movingDirection.x * tornadoSpeed * speedMultiplier, rb.velocity.y, movingDirection.z * tornadoSpeed * speedMultiplier);
        }
        else
        {
            // set the tornado's velocity to be its base speed, in the direction of the movement input.
            rb.velocity = new Vector3(movingDirection.x * tornadoSpeed, rb.velocity.y, movingDirection.z * tornadoSpeed);
        }
    }

    // Check if the tornado is currently grounded by casting a sphere from its ground check transform.
    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
    }

    // Add an upward force to the tornado to make it jump.
    private void Jump()
    {
        // add an upward force to the tornado to make it jump.
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Controlling jingxiao's dust effect
    private void SetDustObjectsActive(bool isActive)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "DustObject")
            {
                child.gameObject.SetActive(isActive);
            }
        }
    }

    void FixedUpdate()
    {
        tornadoMovement();

        /* Horo Says it's no longer needed, go ask Horo
        float currentHeight = transform.position.y;

        // 2 times of the tornado's scale (y

        float maxHeight = transform.localScale.y * 5;

        // Lock the tornado's flying height if it reaches to the maxHeight
        if (currentHeight > maxHeight)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = maxHeight;
            transform.position = newPosition;
        }
        */
    }

    void Update()
    {
        CheckGround();
        //  Jump Check
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if(!isGrounded || (int)rb.velocity.magnitude < 0.1)
        {
            SetDustObjectsActive(false);
        }
        else
        {
            SetDustObjectsActive(true);
        }
        
    }
}

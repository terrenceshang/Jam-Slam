using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class MovementBB : MonoBehaviour
{
    public float speed = 3f; // Speed of the player movement
    public float jumpForce = 10f; // Force of the jump
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component
    private bool isGrounded = false; // To check if the player is on the ground

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movement
        float move = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            move = -1f; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1f; // Move right
        }
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is on the ground
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player has left the ground
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
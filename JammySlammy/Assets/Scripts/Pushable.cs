using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 originalPosition;

    public float pushForce = 4f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    private void Start()
    {
        // If you have any other initialization logic, add it here.
    }

    public void Push(Vector2 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
        // If you have any other logic you want to execute when the object is pushed, you can add it here.
    }

    public void ResetToOriginalPosition()
    {
        transform.position = originalPosition;
        rb.velocity = Vector2.zero; // Stops any movement.
        rb.angularVelocity = 0f;   // Stops any rotation.
    }
}

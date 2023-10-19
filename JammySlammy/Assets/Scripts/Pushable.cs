using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    private Rigidbody2D rb;
    public float pushForce = 4f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Any other initialization logic here
    }

    public void Push(Vector2 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
        // If you have any other logic you want to execute when the object is pushed, you can add it here.
    }
}

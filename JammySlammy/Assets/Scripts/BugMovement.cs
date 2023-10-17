using UnityEngine;

public class BugMovement : MonoBehaviour
{
    public float speed = 2f;
    private int direction = -1; // 1 for right, -1 for left

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Move the bug
        rb.velocity = new Vector2(direction * speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Triggered by: " + collider.gameObject.name);
        if (collider.gameObject.CompareTag("TurnPoint"))
        {
            // Change direction
            direction *= -1;

            // Flip the bug's sprite to face the new direction
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}

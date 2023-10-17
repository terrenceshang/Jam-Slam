using UnityEngine;

public class MovementS : MonoBehaviour
{
    private float horizontal;
    public float speed = 6f;
    public float jumpingPower = 6f;
    private bool isFacingRight = true;
    public float waterSpeedModifier = 0.75f;
    public bool isInWater = false; 
    public float waterJumpModifier = 0.75f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float stepHeight = 0.2f;

    void Update()
    {
        horizontal = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f;
        }

        float currentSpeed = isInWater ? speed * waterSpeedModifier : speed;
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            float actualJumpForce = isInWater ? jumpingPower * waterJumpModifier : jumpingPower;
            rb.velocity = new Vector2(rb.velocity.x, actualJumpForce);
        }

        HandleStepUp();
        Flip();
    }

    private void FixedUpdate()
    {
        float currentSpeed = isInWater ? speed * waterSpeedModifier : speed;
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayer);
        foreach(var collider in colliders)
        {
            if (collider.CompareTag("Ground") || (collider.CompareTag("Player") && collider.gameObject != this.gameObject))
            {
                return true;
            }
        }
        return false;
    }

    private void HandleStepUp()
    {
        float direction = isFacingRight ? 1f : -1f;
        Vector2 rayOrigin = (Vector2)transform.position + new Vector2(direction * 0.5f, 0); 
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, stepHeight + 0.5f, groundLayer); 

        if (hit && Mathf.Abs(hit.point.y - groundCheck.position.y) <= stepHeight)
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + stepHeight, transform.position.z);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

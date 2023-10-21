using UnityEngine;

public class MovementBB : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 8f;
    private bool isFacingRight = true;
    public float waterSpeedModifier = 0.75f;
    public bool isInWater = false;
    public float waterJumpModifier = 0.75f;

    public float superCubeModifier = 1.25f;


    public BoxCollider2D objectBounds;

    public GameObject superCubePrefab;

    public bool hasSpecialCube = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float stepHeight = 0.2f;
    private Camera c;
    private float halfHeight;
    private float halfWidth;
    private float halfSize;

    public Vector3 respawnPoint;

    void Start()
    {
        c = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        halfHeight = c.orthographicSize;
        halfWidth = c.aspect * halfHeight;
        halfSize = (objectBounds.bounds.max.x - objectBounds.bounds.min.x) / 2.0f;
        respawnPoint = transform.position;
    }

    void Update()
    {

        horizontal = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float actualSpeed = hasSpecialCube ? speed * superCubeModifier : speed;

            if (transform.position.x > c.transform.position.x - halfWidth + halfSize)
            {
                horizontal = -1f;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            float actualSpeed = hasSpecialCube ? speed * superCubeModifier : speed;

            if (transform.position.x < c.transform.position.x + halfWidth - halfSize)
            {
                horizontal = 1f;
            }
        }

        float currentSpeed = isInWater ? speed * waterSpeedModifier : speed;
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);



        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            float actualJumpForce = isInWater ? jumpingPower * waterJumpModifier : jumpingPower;

            // Apply SuperCube modifier if hasSpecialCube is true
            actualJumpForce = hasSpecialCube ? actualJumpForce * superCubeModifier : actualJumpForce;

            rb.velocity = new Vector2(rb.velocity.x, actualJumpForce);
        }


        HandleStepUp();
        Flip();

        if (Input.GetKeyDown(KeyCode.DownArrow) && !IsGrounded() && hasSpecialCube)
        {
            Debug.Log("Checking the statement");

            DropSpecialCube();
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = isInWater ? speed * waterSpeedModifier : speed;
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayer);
        foreach (var collider in colliders)
        {
            if ((collider.CompareTag("Ground") || collider.CompareTag("Pushable") || (collider.CompareTag("Player")) && collider.gameObject != this.gameObject))
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

    private void DropSpecialCube()
    {

        Debug.Log("Trying the dropmethod");
        // Position right underneath the character
        Vector2 dropPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);

        GameObject sugarCubeInstance = Instantiate(superCubePrefab, dropPosition, Quaternion.identity);

        // Modify the gravity scale for faster drop
        Rigidbody2D cubeRb = sugarCubeInstance.GetComponent<Rigidbody2D>();
        cubeRb.gravityScale = 5f;  // Adjust the value as per your requirement

        // Start the pickup cooldown on the dropped cube
        sugarCubeInstance.GetComponent<SuperCube>().StartPickupCooldown();

        hasSpecialCube = false; // Set this to false after dropping the cube.
    }

    public void ResetPlayerState()
    {
        isInWater = false;
        // Reset other states or effects as needed
        SpriteRenderer berrySprite = GetComponent<SpriteRenderer>();
        if (berrySprite != null)
        {
            berrySprite.color = new Color(1f, 1f, 1f, 1f); // Reset the sprite's alpha to fully opaque
        }
    }
}
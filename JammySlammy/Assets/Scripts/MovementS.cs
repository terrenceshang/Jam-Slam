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
    public BoxCollider2D objectBounds;

    public GameObject superCubePrefab;

    public bool hasSpecialCube = false;

    public float normalPushForce = 1f; // When no special cube
    public float boostedPushForce = 1000f; // When it has the special cube

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
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > c.transform.position.x - halfWidth + halfSize)
            {
                horizontal = -1f;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < c.transform.position.x + halfWidth - halfSize)
            {
                horizontal = 1f;
            }
        }

        float currentSpeed = isInWater ? speed * waterSpeedModifier : speed;
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            float actualJumpForce = isInWater ? jumpingPower * waterJumpModifier : jumpingPower;
            rb.velocity = new Vector2(rb.velocity.x, actualJumpForce);
        }

        HandleStepUp();
        if (Input.GetKeyDown(KeyCode.S) && !IsGrounded() && hasSpecialCube)
        {
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
            if ((collider.CompareTag("Ground") || collider.CompareTag("Pushable") || collider.CompareTag("Scale") || (collider.CompareTag("Player")) && collider.gameObject != this.gameObject))
            {
                return true;
            }
        }
        return false;
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

    private void DropSpecialCube()
    {
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

    public void setSpecialCubeFalse()
    {
        hasSpecialCube = false;
    }
    public void OnTriggerStay2D(Collider2D other)
    {


        if (other.CompareTag("Pushable"))
        {

            float currentPushForce = hasSpecialCube ? boostedPushForce : normalPushForce;
            Pushable pushableBlock = other.GetComponent<Pushable>();

            if (pushableBlock != null)
            {
                if (Input.GetKey(KeyCode.D))
                {

                    pushableBlock.Push(Vector2.right * currentPushForce);
                }
                else if (Input.GetKey(KeyCode.A))
                {

                    pushableBlock.Push(Vector2.left * currentPushForce);
                }

            }
        }
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
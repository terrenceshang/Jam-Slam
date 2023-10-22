using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GroundMovementSound : MonoBehaviour
{
    public AudioClip movementSound;
    public float playbackSpeed = 1.0f; // Default is normal speed

    private AudioSource audioSource;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius = 0.2f;

    private bool isMoving => Mathf.Abs(rb.velocity.x) > 0.01f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = movementSound;
        audioSource.loop = true;
        audioSource.pitch = playbackSpeed; // Set the playback speed
    }

    private void Update()
    {
        // Update the pitch in case it's changed at runtime
        audioSource.pitch = playbackSpeed;

        if (IsGrounded() && isMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!IsGrounded() || !isMoving)
        {
            audioSource.Stop();
        }
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, groundLayer);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
}

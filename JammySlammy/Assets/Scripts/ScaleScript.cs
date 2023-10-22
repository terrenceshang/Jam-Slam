using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    public GateScript gate;
    public int requiredSugarCubes = 5; 
    public Sprite[] scaleAnimationFrames; 
    public float fps = 10.0f; 

    private SpriteRenderer sr;
    private int currentFrame;
    private bool isAnimating = false;
    private float frameTimer;
    public float offsetChange = 0.5f;
    private BoxCollider2D nonTriggerBoxCollider;
    private float originalOffsetY; 

    private int playersOnScale = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = scaleAnimationFrames[0];
        frameTimer = 1.0f / fps;

        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (var collider in colliders)
        {
            if (!collider.isTrigger)
            {
                nonTriggerBoxCollider = collider;
                break;
            }
        }

        originalOffsetY = nonTriggerBoxCollider.offset.y; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersOnScale++;
            if (playersOnScale == 1) 
            {
                if (Player.sugarCubesCollected >= requiredSugarCubes)
                {
                    gate.OpenGate();
                }
                StartAnimation();
                nonTriggerBoxCollider.offset = new Vector2(nonTriggerBoxCollider.offset.x, nonTriggerBoxCollider.offset.y - offsetChange);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersOnScale--;
            if (playersOnScale <= 0) // If no players are on the scale
            {
                ResetAnimation();
                nonTriggerBoxCollider.offset = new Vector2(nonTriggerBoxCollider.offset.x, originalOffsetY);
            }
        }
    }

    private void StartAnimation()
    {
        currentFrame = -1;
        isAnimating = true;
        frameTimer = 0; 
    }

    private void ResetAnimation()
    {
        isAnimating = false;
        sr.sprite = scaleAnimationFrames[0];
    }

    void Update()
    {
        if (isAnimating)
        {
            frameTimer -= Time.deltaTime;

            if(frameTimer <= 0)
            {
                currentFrame++;
                nonTriggerBoxCollider.offset = new Vector2(nonTriggerBoxCollider.offset.x, originalOffsetY - (currentFrame + 1) * (offsetChange / scaleAnimationFrames.Length));

                if (currentFrame >= scaleAnimationFrames.Length)
                {
                    currentFrame = scaleAnimationFrames.Length - 1;
                    isAnimating = false; 
                }

                sr.sprite = scaleAnimationFrames[currentFrame];
                frameTimer += 1.0f / fps;
            }
        }
    }
}


using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimationController : MonoBehaviour
{
    [Header("Sprite Sequences")]
    public List<Sprite> idleSprites;
    public List<Sprite> moveLeftSprites;
    public List<Sprite> moveRightSprites;
    public List<Sprite> jumpLeftSprites;
    public List<Sprite> jumpRightSprites;
    public float framesPerSecond = 10f;

    private SpriteRenderer spriteRenderer;
    private float frameTimer;
    private int currentFrameIndex;
    private Vector2 previousPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frameTimer = 1.0f / framesPerSecond;
        previousPosition = transform.position;
    }

    void Update()
    {
        HandleAnimation();
        previousPosition = transform.position;
    }

    private void HandleAnimation()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            frameTimer = 1.0f / framesPerSecond;
            currentFrameIndex = (currentFrameIndex + 1) % Mathf.Max(idleSprites.Count, moveLeftSprites.Count, moveRightSprites.Count, jumpLeftSprites.Count, jumpRightSprites.Count);
        }

        float horizontalChange = transform.position.x - previousPosition.x;
        float verticalChange = transform.position.y - previousPosition.y;

        if (Mathf.Abs(verticalChange) < 0.00001 && horizontalChange < -0.0001)
        {

            spriteRenderer.sprite = moveLeftSprites[currentFrameIndex % moveLeftSprites.Count];
        }
        else if (Mathf.Abs(verticalChange) < 0.00001 && horizontalChange > 0.0001)
        {

            spriteRenderer.sprite = moveRightSprites[currentFrameIndex % moveRightSprites.Count];
        }
        else if (Mathf.Abs(verticalChange) > 0.00001)
        {
            if (horizontalChange > 0.0001)
            {
                spriteRenderer.sprite = jumpRightSprites[currentFrameIndex % jumpRightSprites.Count];
            }
            if (horizontalChange < -0.0001)
            {
                spriteRenderer.sprite = jumpLeftSprites[currentFrameIndex % jumpLeftSprites.Count];
            }

        }
        else
        {

            spriteRenderer.sprite = idleSprites[currentFrameIndex % idleSprites.Count];
        }
    }

}

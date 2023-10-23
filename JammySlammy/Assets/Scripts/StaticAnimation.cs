using UnityEngine;

public class StaticAnimation : MonoBehaviour
{
    public Sprite[] cauldronFrames; // Drag your 4 images here in the order you want them to appear
    public float frameDuration = 0.25f; // The duration each frame is shown
    private SpriteRenderer sr;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentFrame = 0;
        timer = frameDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            currentFrame = (currentFrame + 1) % cauldronFrames.Length;
            sr.sprite = cauldronFrames[currentFrame];
            timer = frameDuration;
        }
    }
}

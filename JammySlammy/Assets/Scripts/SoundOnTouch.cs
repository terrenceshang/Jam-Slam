using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class SoundOnTouch : MonoBehaviour
{
    public AudioClip touchSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlaySound();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlaySound();
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !audioSource.isPlaying)
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (touchSound && audioSource)
        {
            audioSource.clip = touchSound;
            audioSource.Play();
        }
    }
}

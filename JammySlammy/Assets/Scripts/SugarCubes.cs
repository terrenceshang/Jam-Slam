using UnityEngine;

public class SugarCube : MonoBehaviour
{
    public SugarCubeManager cubeManager;
    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cubeManager.RegisterPickedUpCube(this, transform.position);
            other.GetComponent<Player>().CollectSugarCube();
            
            StartCoroutine(DeactivateAfterDelay(0.1f)); // Start the coroutine to deactivate the object after 1 second
        }
    }

    System.Collections.IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        gameObject.SetActive(false); // Deactivate the sugar cube after the delay
    }
}

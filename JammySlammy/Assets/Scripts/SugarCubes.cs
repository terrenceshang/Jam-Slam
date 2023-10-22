using UnityEngine;

public class SugarCube : MonoBehaviour
{
    private SugarCubeManager cubeManager;
    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        originalPosition = transform.position;

        cubeManager = FindObjectOfType<SugarCubeManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SugarCubeManager manager = FindObjectOfType<SugarCubeManager>();
            if (manager != null)
            {
                manager.RegisterPickedUpCube(this, transform.position);
            }

            other.GetComponent<Player>().CollectSugarCube();

            spriteRenderer.enabled = false;

            Destroy(gameObject, 3f);
        }
    }
}

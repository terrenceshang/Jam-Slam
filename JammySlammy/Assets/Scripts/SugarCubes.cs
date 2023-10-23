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

            gameObject.SetActive(false); // Deactivate the sugar cube after collection
        }
    }
}

using UnityEngine;

public class SugarCube : MonoBehaviour
{
    private SugarCubeManager cubeManager;
    private Vector3 originalPosition;

    private void Start()
    {
        // Assign the original position when the cube spawns
        originalPosition = transform.position;

        // Find the CubeManager in the scene
        cubeManager = FindObjectOfType<SugarCubeManager>();
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
            Destroy(gameObject); // Destroy the sugar cube after collection
        }
    }

}

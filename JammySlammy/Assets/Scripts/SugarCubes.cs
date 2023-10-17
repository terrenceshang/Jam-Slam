using UnityEngine;

public class SugarCube : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().CollectSugarCube();
            Destroy(gameObject); // Destroy the sugar cube after collection
        }
    }
}

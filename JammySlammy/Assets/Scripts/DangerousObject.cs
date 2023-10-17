using UnityEngine;

public class DangerousObject : MonoBehaviour
{
    public GameObject[] vulnerableObjects; // Array of objects that are vulnerable. Assign these in the inspector.
    public float deathRiseAmount = 0.5f; // The amount the object will rise before falling
    public float deathRiseDuration = 0.5f; // Duration of the rise
    public float deathFallDuration = 2f; // Duration of the fall

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (GameObject vulnerableObj in vulnerableObjects)
        {
            if (other.gameObject == vulnerableObj)
            {
                Debug.Log(vulnerableObj.name + " has encountered the dangerous object and will die.");
                Die(other.gameObject);
                return; // Exit the loop once a match is found
            }
        }
        Debug.Log(other.name + " has encountered the dangerous object but is not affected.");
    }

    private void Die(GameObject obj)
    {
        obj.GetComponent<Collider2D>().enabled = false; // Disable collider to prevent further interactions
        obj.GetComponent<Rigidbody2D>().isKinematic = true; // Set Rigidbody2D to kinematic to control movement
        StartCoroutine(DeathAnimation(obj));
    }

    private System.Collections.IEnumerator DeathAnimation(GameObject obj)
    {
        // Rise up
        float startTime = Time.time;
        Vector3 initialPos = obj.transform.position;
        Vector3 targetPos = initialPos + new Vector3(0, deathRiseAmount, 0);
        while (Time.time - startTime < deathRiseDuration)
        {
            float t = (Time.time - startTime) / deathRiseDuration;
            obj.transform.position = Vector3.Lerp(initialPos, targetPos, t);
            yield return null;
        }

        // Fall down
        startTime = Time.time;
        initialPos = obj.transform.position;
        targetPos = initialPos + new Vector3(0, -10, 0); // Arbitrary large value to ensure it goes off-screen
        while (Time.time - startTime < deathFallDuration)
        {
            float t = (Time.time - startTime) / deathFallDuration;
            obj.transform.position = Vector3.Lerp(initialPos, targetPos, t);
            yield return null;
        }

        Destroy(obj); // Destroy the object
    }
}

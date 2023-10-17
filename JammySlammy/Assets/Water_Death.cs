using UnityEngine;

public class WaterDeath : MonoBehaviour
{
    public GameObject vulnerableBerry; // The berry that dies upon entering the water. Assign this in the inspector.
    public float deathRiseAmount = 0.5f; // The amount the berry will rise before falling
    public float deathRiseDuration = 0.5f; // Duration of the rise
    public float deathFallDuration = 2f; // Duration of the fall

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == vulnerableBerry)
        {
            Debug.Log(vulnerableBerry.name + " has entered the water and will die.");
            Die(other.gameObject);
        }
        else
        {
            Debug.Log(other.name + " has entered the water but is not affected.");
        }
    }

    private void Die(GameObject berry)
    {
        berry.GetComponent<Collider2D>().enabled = false; // Disable collider to prevent further interactions
        berry.GetComponent<Rigidbody2D>().isKinematic = true; // Set Rigidbody2D to kinematic to control movement
        StartCoroutine(DeathAnimation(berry));
    }

    private System.Collections.IEnumerator DeathAnimation(GameObject berry)
    {
        // Rise up
        float startTime = Time.time;
        Vector3 initialPos = berry.transform.position;
        Vector3 targetPos = initialPos + new Vector3(0, deathRiseAmount, 0);
        while (Time.time - startTime < deathRiseDuration)
        {
            float t = (Time.time - startTime) / deathRiseDuration;
            berry.transform.position = Vector3.Lerp(initialPos, targetPos, t);
            yield return null;
        }

        // Fall down
        startTime = Time.time;
        initialPos = berry.transform.position;
        targetPos = initialPos + new Vector3(0, -10, 0); // Arbitrary large value to make sure it goes off-screen
        while (Time.time - startTime < deathFallDuration)
        {
            float t = (Time.time - startTime) / deathFallDuration;
            berry.transform.position = Vector3.Lerp(initialPos, targetPos, t);
            yield return null;
        }

        Destroy(berry); // Destroy the berry object
    }
}

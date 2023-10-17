using UnityEngine;

public class CauldronScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DropBerryIntoCauldron(other.gameObject));
        }
    }

    private System.Collections.IEnumerator DropBerryIntoCauldron(GameObject berry)
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        // Disable the movement script of the berry
        MovementS berryMovement = berry.GetComponent<MovementS>();
        if (berryMovement != null)
        {
            berryMovement.enabled = false;
        }

        float startTime = Time.time;
        float journeyLength = 2f; // 2 seconds to disappear

        Vector3 startScale = berry.transform.localScale;

        while (Time.time < startTime + journeyLength)
        {
            float fracJourney = (Time.time - startTime) / journeyLength;
            berry.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, fracJourney);
            yield return null;
        }

        Destroy(berry); // Destroy the berry after it has disappeared
    }

}

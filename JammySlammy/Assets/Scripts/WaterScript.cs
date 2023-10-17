using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the berries have a tag named "Player"
        {
            Debug.Log(other.name + " has entered the water."); // Print name of the object that entered

            HandleWaterInteraction(other, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name + " has left the water."); // Print name of the object that left

            HandleWaterInteraction(other, false);
        }
    }

    private void HandleWaterInteraction(Collider2D berry, bool isInWater)
    {
        MovementS strawberryMovement = berry.GetComponent<MovementS>();
        MovementBB blueberryMovement = berry.GetComponent<MovementBB>();

        if (strawberryMovement != null)
        {
            strawberryMovement.isInWater = isInWater;
        }

        if (blueberryMovement != null)
        {
            blueberryMovement.isInWater = isInWater;
        }

        SpriteRenderer berrySprite = berry.GetComponent<SpriteRenderer>();
        if (berrySprite != null)
        {
            Color berryColor = berrySprite.color;
            berrySprite.color = new Color(berryColor.r, berryColor.g, berryColor.b, isInWater ? 0.5f : 1f);
        }
    }
}

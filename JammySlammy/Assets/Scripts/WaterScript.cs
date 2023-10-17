    using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the berries have a tag named "Player"
        {
            MovementS berryMovement = other.GetComponent<MovementS>();
            if (berryMovement != null)
            {
                berryMovement.isInWater = true;
                SpriteRenderer berrySprite = other.GetComponent<SpriteRenderer>();
                Color berryColor = berrySprite.color;
                berrySprite.color = new Color(berryColor.r, berryColor.g, berryColor.b, 0.5f); // 50% opacity
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MovementS berryMovement = other.GetComponent<MovementS>();
            if (berryMovement != null)
            {
                berryMovement.isInWater = false;
                SpriteRenderer berrySprite = other.GetComponent<SpriteRenderer>();
                Color berryColor = berrySprite.color;
                berrySprite.color = new Color(berryColor.r, berryColor.g, berryColor.b, 1f); // back to full opacity
            }
        }
    }
}

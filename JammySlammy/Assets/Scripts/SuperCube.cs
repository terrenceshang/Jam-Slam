using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCube : MonoBehaviour
{
    public SuperCubeManager cubeManager;
    private float pickupCooldown = 0f; // Time until the cube can be picked up again
    private const float CooldownDuration = 0.5f; // The duration of the cooldown

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pickupCooldown <= 0f && other.CompareTag("Player"))
        {
            cubeManager.RegisterPickedUpCube(this, transform.position);
            AssignCubeToBerry(other);
            StartCoroutine(DeactivateAfterDelay(0.1f)); // Start the coroutine to delay deactivation
        }
        if (other.CompareTag("Ground"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.isKinematic = true;  // Makes the cube unaffected by external forces.
            }
        }
    }

    void Update()
    {
        if (pickupCooldown > 0f)
        {
            pickupCooldown -= Time.deltaTime;
        }
    }

    private void AssignCubeToBerry(Collider2D berry)
    {
        MovementS strawberryMovement = berry.GetComponent<MovementS>();
        MovementBB blueberryMovement = berry.GetComponent<MovementBB>();

        if (strawberryMovement != null && !strawberryMovement.hasSpecialCube)
        {
            Debug.Log("Strawberry picked up the special cube!");
            strawberryMovement.hasSpecialCube = true; // Assign the special cube to strawberry
        }
        else if (blueberryMovement != null && !blueberryMovement.hasSpecialCube)
        {
            Debug.Log("Blueberry picked up the special cube!");
            blueberryMovement.hasSpecialCube = true; // Assign the special cube to blueberry
        }
    }

    public void StartPickupCooldown()
    {
        pickupCooldown = CooldownDuration;
    }

    IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}

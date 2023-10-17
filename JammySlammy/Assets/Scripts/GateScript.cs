using UnityEngine;

public class GateScript : MonoBehaviour
{
    public GameObject closedGate; // Drag the closed gate sprite or game object here
    public GameObject openGate;   // Drag the open gate sprite or game object here
    public BoxCollider2D gateCollider; // Drag the collider of the closed gate here

    public void OpenGate()
    {
        closedGate.SetActive(false); // Makes the closed gate disappear
        openGate.SetActive(true);    // Makes the open gate appear
        gateCollider.enabled = false; // Disables the collider so the player can pass through
    }

    // Removed the CloseGate method as we no longer need to close the gate
}


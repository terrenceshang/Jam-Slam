using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float x;

    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    private void Update()
    {
        if (player1 == null || player2 == null)
        {
            return; // Exit if any player has been destroyed or is not assigned yet
        }

        x = (player1.position.x + player2.position.x) / 2.0f;
        if (x > 7.02)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }

    // Functions to update player references
    public void RegisterPlayer1(Transform playerTransform)
    {
        player1 = playerTransform;
    }

    public void RegisterPlayer2(Transform playerTransform)
    {
        player2 = playerTransform;
    }
}

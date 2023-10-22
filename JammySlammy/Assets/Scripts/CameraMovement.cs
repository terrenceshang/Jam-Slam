using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float boundaryBoxWidth = 5.0f;
    [SerializeField] private float smoothSpeed = 0.125f;  // Adjust the smoothing speed

    private void FixedUpdate()
    {
<<<<<<< Updated upstream
        if (player1 == null || player2 == null)
        {
            return; // Exit if any player has been destroyed or is not assigned yet
        }

        x = (player1.position.x + player2.position.x) / 2.0f;
        if (x > 7.02)
=======
        float cameraCenterX = transform.position.x;
        float leftBoundary = cameraCenterX - boundaryBoxWidth / 2;
        float rightBoundary = cameraCenterX + boundaryBoxWidth / 2;

        bool player1OutOfBox = player1.position.x < leftBoundary || player1.position.x > rightBoundary;
        bool player2OutOfBox = player2.position.x < leftBoundary || player2.position.x > rightBoundary;
        bool playersOnSameSide = (player1.position.x - cameraCenterX) * (player2.position.x - cameraCenterX) > 0;

        if ((player1OutOfBox || player2OutOfBox) && playersOnSameSide)
>>>>>>> Stashed changes
        {
            float averageX = (player1.position.x + player2.position.x) / 2;
            Vector3 targetPosition = new Vector3(averageX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
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

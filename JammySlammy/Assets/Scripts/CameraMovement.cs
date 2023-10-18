using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    private float x;

    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    private void Update()
    {
        x = (player1.position.x + player2.position.x) / 2.0f;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}

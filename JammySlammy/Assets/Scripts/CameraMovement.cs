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
        x = (player1.position.x + player2.position.x) / 2.0f;
        if (x > 7.02)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovement : MonoBehaviour
{
    private Camera c;

    void Start()
    {
        c = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(c.transform.position.x, transform.position.y, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelClick : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite barrel;
    public Sprite glowBarrel;
    private Camera c;

    void Start()
    {
        c = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = barrel;
    }

    void OnMouseEnter()
    {
        spriteRenderer.sprite = glowBarrel;
    }

    void OnMouseDown()
    {
        c.transform.position = new Vector3(7.02f, 10.49f, -12f);
    }

    void OnMouseExit()
    {
        spriteRenderer.sprite = barrel;
    }
}

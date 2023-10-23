using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite defaultSprite;  // Renamed for clarity
    public Sprite hoverSprite;    // Renamed for clarity
    public string sceneName;      // Scene name to load

    private Camera c;

    void Start()
    {
        c = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
    }

    void OnMouseEnter()
    {
        spriteRenderer.sprite = hoverSprite;
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name not set for button: " + gameObject.name);
        }
    }

    void OnMouseDown()
    {
        LoadScene();
    }

    void OnMouseExit()
    {
        spriteRenderer.sprite = defaultSprite;
    }
}

using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static int sugarCubesCollected = 0;
    public int sugarCubesRequired = 5;
    public GateScript gate;
    public TextMeshProUGUI sugarCubeCounterUI;

    private void Update()
    {
        sugarCubeCounterUI.text = "Sugar Cubes: " + sugarCubesCollected + "/" + sugarCubesRequired;
    }

    public void CollectSugarCube()
    {
        sugarCubesCollected++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Scale") && sugarCubesCollected >= sugarCubesRequired)
        {
            gate.OpenGate();
        }
    }
}
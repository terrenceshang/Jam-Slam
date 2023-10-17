using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static int sugarCubesCollected = 0; // Current count (now static)
    public int sugarCubesRequired = 5; // Set this to the required number to open the gate
    public GateScript gate; // Drag the Gate object with the GateScript here
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
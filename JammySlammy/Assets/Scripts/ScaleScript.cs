using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    public GateScript gate; 
    public int requiredSugarCubes = 5; // Set this to the number of sugar cubes needed to open the gate

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Player.sugarCubesCollected >= requiredSugarCubes) // Using the class name to access the static variable
            {
                gate.OpenGate();
            }
        }
    }
}

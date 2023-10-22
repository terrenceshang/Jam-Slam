using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarCubeManager : MonoBehaviour

{
    // A dictionary to keep track of the cubes that were picked up and their original positions
    private Dictionary<SugarCube, Vector3> pickedUpCubes = new Dictionary<SugarCube, Vector3>();

    // Call this method when a cube is picked up
    public void RegisterPickedUpCube(SugarCube cube, Vector3 originalPosition)
    {
        if (!pickedUpCubes.ContainsKey(cube))
        {
            pickedUpCubes.Add(cube, originalPosition);
        }
    }

    // Call this method when a player dies
    public void RespawnCubes()
    {
        Debug.Log("Inni manager sc");
        foreach (var pair in pickedUpCubes)
        {
            SugarCube cube = pair.Key;
            Vector3 originalPosition = pair.Value;

            // Instantiate a new cube at the original position
            SugarCube respawnedCube = Instantiate(cube, originalPosition, Quaternion.identity);


        }

        // Clear the picked up cubes list
        pickedUpCubes.Clear();
    }
}

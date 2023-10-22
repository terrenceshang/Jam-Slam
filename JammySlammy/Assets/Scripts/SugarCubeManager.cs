using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarCubeManager : MonoBehaviour

{
    // A dictionary to keep track of the cubes that were picked up and their original positions
    private Dictionary<SugarCube, Vector3> pickedUpCubes = new Dictionary<SugarCube, Vector3>();

    public SugarCube sugarCubePrefab;

    // Call this method when a cube is picked up
    public void RegisterPickedUpCube(SugarCube cube, Vector3 originalPosition)
    {
        Debug.Log("REGISTRERT PLUKKET OPP");
        if (!pickedUpCubes.ContainsKey(cube))
        {
            pickedUpCubes.Add(cube, originalPosition);
            Debug.Log("Current size of pickedUpCubes: " + pickedUpCubes.Count);
        }
    }


    // Call this method when a player dies
    // Call this method when a player dies
    public void RespawnCubes()
    {


        // Check the count of sugar cubes to respawn
        Debug.Log("Number of cubes to respawn: " + pickedUpCubes.Count);

        foreach (var pair in pickedUpCubes)
        {
            Debug.Log("11111INNE I RESPAWN CUBES: SKJER DET NOE HER???");
            Vector3 originalPosition = pair.Value;

            // Instantiate a new cube at the original position using the prefab
            SugarCube respawnedCube = Instantiate(sugarCubePrefab, originalPosition, Quaternion.identity);
            Debug.Log("INNE I RESPAWN CUBES: SKJER DET NOE HER???");
        }

        // Clear the picked up cubes list
        pickedUpCubes.Clear();
    }






}

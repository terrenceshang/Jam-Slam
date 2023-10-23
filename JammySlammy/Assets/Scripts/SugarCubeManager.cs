using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarCubeManager : MonoBehaviour

{
    // A dictionary to keep track of the cubes that were picked up and their original positions


    public SugarCube sugarCubePrefab;



    public List<SugarCube> deactivatedCubes = new List<SugarCube>();

    public void RegisterPickedUpCube(SugarCube cube, Vector3 originalPosition)
    {

        if (!deactivatedCubes.Contains(cube))
        {
            deactivatedCubes.Add(cube);
            Debug.Log("Current number of deactivated cubes: " + deactivatedCubes.Count);
        }
    }

    public void RespawnCubes()
    {


        foreach (SugarCube cube in deactivatedCubes)
        {

            cube.gameObject.SetActive(true); // Reactivate the sugar cube
        }

        // Clear the deactivated cubes list
        deactivatedCubes.Clear();
    }

    public void nOfCubes()
    {
        Debug.Log("BarenymetodeNumber of cubes to respawn: " + deactivatedCubes.Count);

    }





}

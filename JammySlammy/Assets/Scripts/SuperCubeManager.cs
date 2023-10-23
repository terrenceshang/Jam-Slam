using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCubeManager : MonoBehaviour
{
    public SuperCube SuperCubePrefab;



    public List<SuperCube> deactivatedCubes = new List<SuperCube>();

    public void RegisterPickedUpCube(SuperCube cube, Vector3 originalPosition)
    {
        Debug.Log("REGISTRERT PLUKKET OPP");

        deactivatedCubes.Add(cube);
        Debug.Log("Current number of deactivated cubes: " + deactivatedCubes.Count);

    }

    public void RespawnCubes()
    {
        Debug.Log("Number of cubes to respawn: " + deactivatedCubes.Count);

        foreach (SuperCube cube in deactivatedCubes)
        {
            Debug.Log("Deaktiverr nå");
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

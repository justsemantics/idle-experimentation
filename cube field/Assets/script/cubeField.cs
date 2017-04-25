using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cubeField : MonoBehaviour {

    [SerializeField]
    GameObject cubePrefab;

    [SerializeField]
    int dimension;

    List<GameObject> activeCubes = new List<GameObject>();

    GameObject[,,] allCubes;

	// Use this for initialization
	void Start () {
        allCubes = new GameObject[dimension, dimension, dimension];

        for (int x = 0; x < dimension; x++)
        {
            for (int y = 0; y < dimension; y++)
            {
                for (int z = 0; z < dimension; z++)
                {
                    GameObject newCube = Instantiate(cubePrefab);

                    newCube.transform.position = new Vector3(x, y, z);

                    allCubes[x, y, z] = newCube;

                    newCube.SetActive(false);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<GameObject> getCubesNear(Vector3 location, float radius)
    {
        List<GameObject> cubesToReturn = new List<GameObject>();

        int minX, maxX, minY, maxY, minZ, maxZ;

        //convert bounds to integers
        minX = Mathf.FloorToInt(location.x - radius);
        maxX = Mathf.CeilToInt(location.x + radius);

        minY = Mathf.FloorToInt(location.y - radius);
        maxY = Mathf.CeilToInt(location.y + radius);

        minZ = Mathf.FloorToInt(location.z - radius);
        maxZ = Mathf.CeilToInt(location.z + radius);

        //clamp to avoid index out of range on the cube array
        minX = Mathf.Clamp(minX, 0, dimension);
        maxX = Mathf.Clamp(maxX, 0, dimension);

        minY = Mathf.Clamp(minY, 0, dimension);
        maxY = Mathf.Clamp(maxY, 0, dimension);

        minZ = Mathf.Clamp(minZ, 0, dimension);
        maxZ = Mathf.Clamp(maxZ, 0, dimension);

        //add all the cubes we might need to the list
        for(int x = minX; x < maxX; x++)
        {
            for(int y = minY; y < maxY; y++)
            {
                for(int z = minZ; z < maxZ; z++)
                {
                    cubesToReturn.Add(allCubes[x, y, z]);
                }
            }
        }

        foreach(GameObject g in activeCubes) // turn off all the old cubes
        {
            g.SetActive(false);
        }

        activeCubes.Clear();

        foreach (GameObject g in cubesToReturn) //turn on all the cubes in range
        {
            g.SetActive(true);

            activeCubes.Add(g);
        }

        return cubesToReturn;
    }
}

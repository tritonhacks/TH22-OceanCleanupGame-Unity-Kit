using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trash;
    public GameObject boat;
    public Camera cam;

    public LayerMask floorLayer;
    public float minSpawnDist;
    public float screenEdgeOffset;

    // Corners of screen on water
    private Vector3 upperLeft;
    private Vector3 lowerLeft;
    private Vector3 upperRight;
    private Vector3 lowerRight;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        calculateCorners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnNewTrash()
    {
        // Set spawn coordinates
        Vector3 randomPos = createRandomPos();

        // Spawn new trash
        Instantiate(trash[Random.Range(0, trash.Length)], randomPos, Quaternion.identity);
    }

    private Vector3 createRandomPos()
    {
        bool validSpawn = false;
        int iter = 0;
        float randomX = 0f;
        float randomZ = 0f;
        
        // Create new spawn locations until one is valid or max iterations reached
        while(validSpawn == false && iter <= 5)
        {
            // Calculate spawn position offset
            randomX = Random.Range(upperLeft.x + screenEdgeOffset, upperRight.x - screenEdgeOffset);
            randomZ = Random.Range(lowerLeft.z + screenEdgeOffset, upperLeft.z - screenEdgeOffset);

            validSpawn = spawnCheck(randomX, randomZ);
            iter++;
        }       

        return new Vector3(randomX, 0.25f, randomZ);
    }

    private bool spawnCheck(float x, float z)
    {
        bool clear = true;

        // Check distance from boat, mark if too close
        if (Math.Abs(x - boat.transform.position.x) <= minSpawnDist &&
            Math.Abs(z - boat.transform.position.z) <= minSpawnDist)
            clear = false;


        // Check distance from screen edges, mark if too close
        if (x - upperLeft.x <= minSpawnDist)
            clear = false; //Left edge
            
        if (upperRight.x - x <= minSpawnDist) //Right edge
            clear = false; //Right edge
        if (upperLeft.z - z <= minSpawnDist) //Top edge
            clear = false; //Top edge
        if (z - lowerLeft.z <= minSpawnDist) //Bottom edge
            clear = false; //Bottom edge

        return clear;
    }

    private void calculateCorners()
    {
        RaycastHit hit;
        Ray upperLeftRay = cam.ScreenPointToRay(new Vector2(0, Screen.height));
        Ray lowerLeftRay = cam.ScreenPointToRay(new Vector2(0, 0));
        Ray upperRightRay = cam.ScreenPointToRay(new Vector2(Screen.width, Screen.height));
        Ray lowerRightRay = cam.ScreenPointToRay(new Vector2(Screen.width, 0));

        if (Physics.Raycast(upperLeftRay, out hit, 100f, floorLayer))
        {
            upperLeft = hit.point;
        }

        if (Physics.Raycast(lowerLeftRay, out hit, 100f, floorLayer))
        {
            lowerLeft = hit.point;
        }

        if (Physics.Raycast(upperRightRay, out hit, 100f, floorLayer))
        {
            upperRight = hit.point;
        }

        if (Physics.Raycast(lowerRightRay, out hit, 100f, floorLayer))
        {
            lowerRight = hit.point;
        }
    }
}

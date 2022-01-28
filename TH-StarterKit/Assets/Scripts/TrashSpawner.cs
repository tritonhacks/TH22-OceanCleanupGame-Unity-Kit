using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float sceneWidth;
    private float sceneHeight;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        calculateCorners();

        // Spawn trash at start of game
        // TODO: If doing UI, move to UI script for when game starts
        spawnNewTrash();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnNewTrash()
    {
        // Calculate spawn position offset
        float randomX = Random.Range(screenEdgeOffset, sceneWidth - screenEdgeOffset);
        float randomZ = Random.Range(screenEdgeOffset, sceneHeight - screenEdgeOffset);

        // Check distance from boat, adjust if too close
        if (upperLeft.x + randomX - boat.transform.position.x <= minSpawnDist)
            randomX = (randomX + (sceneWidth/2)) % sceneWidth;
        if (lowerLeft.z + randomZ - boat.transform.position.z <= minSpawnDist)
            randomZ = (randomZ + (sceneHeight/2)) % sceneHeight;

        // Set spawn coordinates
        Vector3 randomPos = new Vector3(upperLeft.x + randomX, 0.5f, lowerLeft.z + randomZ);

        // Spawn new trash
        Instantiate(trash[Random.Range(0, trash.Length)], randomPos, Quaternion.identity);
    }

    private void calculateCorners()
    {
        // Bottom left of screen defined at (0,0), top right at (Screen.width, Screen.height)
        Vector3 upperLeftScreen = cam.ScreenToWorldPoint(new Vector2 (0, Screen.height));
        Vector3 lowerLeftScreen = cam.ScreenToWorldPoint(new Vector2(0, 0));
        Vector3 upperRightScreen = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector3 lowerRightScreen = cam.ScreenToWorldPoint(new Vector2(Screen.width, 0));

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

        sceneHeight = upperLeft.z - lowerLeft.z;
        sceneWidth = upperRight.x - upperLeft.x;
    }
}

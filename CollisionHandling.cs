using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandling : MonoBehaviour
{
    int trashHeld = 0;
    //Need to HardCode this with location of Port
    int diskSpawnX = 0;
    int diskSpawnY = 0;
    int diskSpawnZ = 0;
    bool diskSpawned = false;
    // Start is called before the first frame update
    void Start()
    {

    }


    //Delete Collected Trash (up to 10 items) and Drops off Trash at Port
    void OnCollisionEnter(Collision collision)
    {
        //Collects trash until 10 items
        if (trashHeld <= 9)
        {
            if (collision.gameObject.tag == "Trash")
            {
                trashHeld++;
                Destroy(collision.gameObject);
            }
        }
    }


    //Spawns Disk at Location diskSpawnX, diskSpawnY, diskSpawnZ and Colors it
    void CreateDropOffLocation()
    {
        GameObject dropOffLocation = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dropOffLocation.transform.position = new Vector3(diskSpawnX, diskSpawnY, diskSpawnZ);
        dropOffLocation.transform.localScale = new Vector3((float)15, (float)0.5, (float)15);

        //Changes Color of Disk to Red
        var diskRenderer = dropOffLocation.GetComponent<Renderer>();
        diskRenderer.material.SetColor("_Color", Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        //If ship is full, create Drop Off Location
        if (!diskSpawned &&  trashHeld >= 10)
        {
            diskSpawned = true;
            CreateDropOffLocation();
        }
    }
}

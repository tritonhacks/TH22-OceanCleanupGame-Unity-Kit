using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandling : MonoBehaviour
{
    public int maxStorage;
    private int trashHeld = 0;

    //Need to HardCode this with location of Port
    [SerializeField] float diskSpawnX = 0;
    [SerializeField] float diskSpawnY = 0;
    [SerializeField] float diskSpawnZ = 0;
    private bool diskSpawned = false;

    public TrashSpawner ts;

    // Start is called before the first frame update
    void Start()
    {
        ts = FindObjectOfType<TrashSpawner>();
    }


    //Delete Collected Trash
    void OnCollisionEnter(Collision collision)
    {
        //Collects trash until storage full
        if (trashHeld < maxStorage)
        {
            Debug.Log("Hit Object");
            if (collision.gameObject.tag == "Trash")
            {
                trashHeld++;
                Destroy(collision.gameObject);
                ts.spawnNewTrash();
            }
        }
    }


    //Spawns Disk at Location diskSpawnX, diskSpawnY, diskSpawnZ and Colors it
    void CreateDropOffLocation()
    {
        GameObject dropOffLocation = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dropOffLocation.transform.position = new Vector3(diskSpawnX, diskSpawnY, diskSpawnZ);
        dropOffLocation.transform.localScale = new Vector3(15f, 0.5f, 15f);

        //Changes Color of Disk to Red
        var diskRenderer = dropOffLocation.GetComponent<Renderer>();
        diskRenderer.material.SetColor("_Color", Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        //If ship is full, create Drop Off Location
        if (!diskSpawned &&  trashHeld >= maxStorage)
        {
            diskSpawned = true;
            CreateDropOffLocation();
        }
    }
}

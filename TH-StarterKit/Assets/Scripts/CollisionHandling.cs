using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandling : MonoBehaviour
{
    public int maxCapacity;
    private int trashHeld = 0;
    private bool spawnTrash = false;

    private bool dockSpawned = false;

    public GameObject dock;
    public Vector3 spawnL, spawnR;

    public TrashSpawner ts;
    public UIHandling ui;

    // Start is called before the first frame update
    void Start()
    {
        ts = FindObjectOfType<TrashSpawner>();
        ui = FindObjectOfType<UIHandling>();

        // Setup dock as not spawned
        dock.SetActive(false);
    }


    //Delete Collected Trash
    void OnCollisionEnter(Collision collision)
    {

        if (trashHeld == maxCapacity && collision.gameObject.tag == "Dock")
        {
            trashHeld = 0;
            ui.ResetTrashCount();
            dock.SetActive(false);
            dockSpawned = false;
        }
        //Collects trash until storage full
        
        if (trashHeld < maxCapacity)
        {
            if (collision.gameObject.tag == "Trash")
            {
                trashHeld++;
                ui.IncrementTrashCount();
                Destroy(collision.gameObject);
                spawnTrash = true;
            }
        }

        
    }

    void SetDropOffLocation()
    {
        // Have dock spawn on opposite side of boat to prevent dock spawning in boat
        if (this.transform.position.x < Camera.main.transform.position.x)
            dock.transform.position = spawnR;
        else
            dock.transform.position = spawnL;

        dock.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //If ship is full, create Drop Off Location
        if (!dockSpawned && trashHeld == maxCapacity)
        {
            ui.SetFullTextActive(true);
            dockSpawned = true;
            SetDropOffLocation();
        }

        else if (spawnTrash && trashHeld < maxCapacity)
        {
            ts.spawnNewTrash();
            spawnTrash = false;
        }
    }
}

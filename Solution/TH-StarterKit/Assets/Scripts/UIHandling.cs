using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandling : MonoBehaviour
{
    public GameObject boat;

    private int trashHeld = 0;
    private int maxCapacity;

    private BoatMovement bm;
    private CollisionHandling ch;
    private TrashSpawner ts;

    public Text trashHeldText;
    public Text trashCapacityText;
    public Text startGameText;
    public Text boatFullText;

    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        bm = boat.GetComponent<BoatMovement>();
        ch = boat.GetComponent<CollisionHandling>();
        ts = FindObjectOfType<TrashSpawner>();

        maxCapacity = ch.maxCapacity;

        // Initialize text values
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameStarted == false)
            StartGame();
    }

    public void StartGame()
    {
        gameStarted = true;

        startGameText.gameObject.SetActive(false);
        trashHeldText.gameObject.SetActive(true);
        trashCapacityText.gameObject.SetActive(true);

        bm.StartGame();
        ts.spawnNewTrash();
    }

    public void IncrementTrashCount()
    {
        trashHeld++;
        UpdateText();
    }

    public void ResetTrashCount()
    {
        trashHeld = 0;
        SetFullTextActive(false);
        UpdateText();
    }

    public void SetFullTextActive(bool active)
    {
        boatFullText.gameObject.SetActive(active);
    }

    private void UpdateText()
    {
        trashHeldText.text = "Trash: " + trashHeld;
        trashCapacityText.text = "Capacity: " + maxCapacity;
    }
}

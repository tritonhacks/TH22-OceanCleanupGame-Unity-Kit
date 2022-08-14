using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;

    [Header("Movement")]
    public float speed;
    public float rotation_speed;
    public float drag;
    public float minDist;

    [Header("Raycast")]
    public LayerMask floorLayer;
    private Vector3 targetPos;

    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted)
        {
            //drag
            rb.drag = drag;

            //Raycast from mouse
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, floorLayer))
            {
                targetPos = hit.point;
            }

            float distance = Vector3.Distance(transform.position, targetPos);
            Vector3 direction = (targetPos - transform.position).normalized;

            if (distance > minDist)
            {
                //rotation
                Quaternion rot = Quaternion.LookRotation(direction);
                Quaternion newrot = Quaternion.Euler(0, rot.eulerAngles.y, 0); //ensure rotation only happens in y axis

                transform.rotation = Quaternion.Lerp(transform.rotation, newrot, rotation_speed * Time.deltaTime);

                //movement
                rb.AddForce(transform.forward * speed);

            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}

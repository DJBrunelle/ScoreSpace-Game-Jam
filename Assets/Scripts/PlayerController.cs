using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    public KeyCode thrustKey;
    public KeyCode brakeKey;
    public KeyCode shootKey;

    Camera cam;

    // Start is called before the first frame update
    


    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        FaceMouse();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(thrustKey))
        {
            Move(transform.up * -1);
        }
        if (Input.GetKey(brakeKey))
        {
            Brake();
        }
    }

    void FaceMouse()
    {
        Vector3 mouseLocation = Input.mousePosition;
        mouseLocation.z = 0;

        Vector3 shipLocation = cam.WorldToScreenPoint(transform.position);
        shipLocation.z = 0;

        Vector3 relPos = shipLocation - mouseLocation;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, relPos);    
    }
}



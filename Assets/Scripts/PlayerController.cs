using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    public KeyCode thrustKey;
    public KeyCode brakeKey;
    public KeyCode shootKey;
    public int fireRate;

    float timeSinceFire;

    Camera cam;
    
    public GameObject laser;

    // Start is called before the first frame update
    


    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        timeSinceFire += Time.deltaTime;

        FaceMouse();

        if (Input.GetKey(shootKey))
        {
            if (timeSinceFire > (1f/fireRate))
            {
                Fire();
            }
        }

        UpdateHealth();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(thrustKey))
        {
            Move(transform.up);
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

        transform.rotation = Quaternion.LookRotation(Vector3.back, -relPos);    
    }

    void Fire()
    {
        var pool = GameObject.FindGameObjectsWithTag("Laser");

        LaserController usedLaser = null;

        for (int ii = 0 ; ii < pool.Length; ii++)
        {
            usedLaser = pool[ii].GetComponent<LaserController>();

            if (usedLaser.inPool)
            {
                break;
            }
        }

        GameObject laserShot = null;

        if (usedLaser != null && usedLaser.inPool)
        {
            laserShot = usedLaser.gameObject;
        }

        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 1);

        if (laserShot == null)
        {
            Instantiate(laser, spawnPos, transform.rotation, GameObject.Find("LaserPool").transform);
        } else
        {
            laserShot.transform.position = spawnPos;
            laserShot.transform.rotation = transform.rotation;
            laserShot.GetComponent<LaserController>().inPool = false;
        }

        timeSinceFire = 0f;
    }
}



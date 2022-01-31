using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{

    AudioSource[] audioSources;


    public KeyCode thrustKey;
    public KeyCode brakeKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode shootKey;
    public int fireRate;

    float timeSinceFire;

    Camera cam;

    List<Powerup> powerups = new List<Powerup>();
    
    public GameObject laser;

    new void Awake()
    {
        base.Awake();
        audioSources = GetComponents<AudioSource>();
    }

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
        CheckPowerups();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(thrustKey))
        {
            Move(transform.up);
        } else if (Input.GetKey(brakeKey))
        {
            Move(-transform.up);
        } else if (Input.GetKey(rightKey))
        {
            Move(transform.right);
        } else if (Input.GetKey(leftKey))
        {
            Move(-transform.right);
        } else
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
            laserShot.GetComponent<LaserController>().ReFire(transform.position,transform.rotation);
        }

        timeSinceFire = 0f;
    }

    public void Powerup(int multiplier, Stat stat, int duration)
    {
        if (multiplier == 0 || duration == 0)
        {
            return;
        }
        Powerup newPower = new Powerup(multiplier, stat,duration);

        if (stat == Stat.FireRate)
        {
            fireRate = newPower.Activate(fireRate);
        } else if (stat == Stat.Speed)
        {
            speed = newPower.Activate(speed);
        }
        powerups.Add(newPower);
    }

    public new void Move(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    public void CheckPowerups()
    {
        for (int ii = 0; ii < powerups.Count; ii++)
        {
            var power = powerups[ii];
            power.Use();
            if (!power.IsActive())
            {
                if (power.stat == Stat.FireRate)
                {
                    fireRate = power.Deactivate(fireRate);
                } else if (power.stat == Stat.Speed)
                {
                    speed = power.Deactivate(speed);  
                }
                powerups.Remove(power);
            }
        }
    }
}

public enum Stat
{
    Speed,
    FireRate
};

public class Powerup
{
    public int multiplier;
    public Stat stat;
    public float duration;

    public Powerup(int mult, Stat shipStat, int setDuration)
    {
        multiplier = mult;
        stat = shipStat;
        duration = (float)setDuration;
    }

    public void Use()
    {
        duration -= Time.deltaTime;
    }

    public int Activate(int incomingStat)
    {
        return (int) incomingStat * multiplier;
    }

    public bool IsActive()
    {
        if (duration <= 0)
        {
            return false;
        }
        return true;
    }

    public int Deactivate(int stat)
    {
        return (int)(stat / multiplier);
    }
}

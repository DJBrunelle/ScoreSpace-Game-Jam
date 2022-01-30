using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Actor
{
    protected StationController station;
    public int speed;
    public int damage;

    protected Rigidbody2D rb;
    protected Vector3 stationPos;
    // Start is called before the first frame update
    void Awake()
    {
        station = GameObject.Find("Station").GetComponent<StationController>();
        rb = GetComponent<Rigidbody2D>();
        stationPos = station.gameObject.transform.position; 
        LoadHealth();
    }

    public void Move(Vector2 direction)
    {
        rb.AddForce(speed * direction);
    }

    public void Brake()
    {
        rb.AddForce(-(rb.velocity.normalized) * speed);
    }

    public void DoDamage(Actor target)
    {
        
    }
}

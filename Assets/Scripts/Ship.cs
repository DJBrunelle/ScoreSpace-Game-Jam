using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Actor
{
    public int speed;
    public int damage;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

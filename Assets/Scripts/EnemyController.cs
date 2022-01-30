using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Ship
{
    public GameController gameController;
    public EnemyType type;

    public int damageRate;

    public int points;

    float timeSinceDamage;

    new void Awake()
    {
        base.Awake();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        if (type == EnemyType.Bomber)
        {
            BomberAttack();
        } else if (type == EnemyType.Ranged)
        {
            RangerAttack();
        }
    }

        // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        if (isDead())
        {
            gameController.AddToScore(points);
            Destroy(gameObject);
        }
    }


    void FaceStation()
    {
        stationPos.z = 0;

        Vector3 shipLocation = transform.position;
        shipLocation.z = 0;

        Vector3 relPos = shipLocation - stationPos;

        transform.rotation = Quaternion.LookRotation(Vector3.back, -relPos); 
    }

    void BomberAttack()
    {
        FaceStation();

        if (rb.velocity.magnitude < speed)
        {
            Move(transform.up * speed * Time.fixedDeltaTime);
        }

    }

    void RangerAttack()
    {
        float dist = Vector3.Distance(transform.position, stationPos);

        FaceStation();

        if(dist < 5)
        {
            rb.velocity = rb.velocity * 0.95f;

            if (timeSinceDamage > (1f / damageRate))
            {
                station.TakeDamage(damage);
                timeSinceDamage = 0;
            }
            timeSinceDamage += Time.fixedDeltaTime;
        } else if (rb.velocity.magnitude < speed)
        {
            Move(transform.up * speed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Friendly")
        {
            col.gameObject.GetComponent<Actor>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
    }
}

public enum EnemyType
{
    Bomber,
    Ranged
};

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Ship
{
    AudioSource[] audioSources;
    AudioSource collisionSound;
    AudioSource explosionSound;
    AudioSource rangedSound;

    public GameController gameController;
    public GameObject explosion;
    public ArenaController arenaController;
    public EnemyType type;

    Animator anim;
    public GameObject fazer;

    public int damageRate;

    public int points;

    float timeSinceDamage;

    new void Awake()
    {
        base.Awake();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        arenaController = GameObject.Find("Arena").GetComponent<ArenaController>();

        audioSources = GetComponents<AudioSource>();

        collisionSound = audioSources[0];
        explosionSound = audioSources[1];
        if (type == EnemyType.Ranged)
        {
            rangedSound = audioSources[2];
        }

        anim = GetComponent<Animator>();
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
        if (gameController.gameOver && type == EnemyType.Ranged)
        {
            rangedSound.Stop();
        }
        UpdateHealth();
        if (isDead())
        {
            arenaController.PowerupSpawn(transform.position);
            gameController.AddToScore(points);
            explosionSound.Play();
            Instantiate(explosion, transform.position,Quaternion.identity);
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

        if(dist < 3.5f)
        {
            anim.SetBool("isAttacking", true);
            fazer.SetActive(true);
            rb.velocity = rb.velocity * 0.95f;

            if (timeSinceDamage > (1f / damageRate))
            {
                station.TakeDamage(damage);
                timeSinceDamage = 0;
            }
            timeSinceDamage += Time.fixedDeltaTime;
            if(!rangedSound.isPlaying)
            {
                rangedSound.Play();
            }
        } else if (rb.velocity.magnitude < speed)
        {
            anim.SetBool("isAttacking", false);
            Move(transform.up * speed * Time.fixedDeltaTime);
            rangedSound.Stop();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Friendly")
        {
            col.gameObject.GetComponent<Actor>().TakeDamage(damage);
            collisionSound.Play();
            Instantiate(explosion, transform.position,Quaternion.identity);
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

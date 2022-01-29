using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public PlayerController player;
    public ArenaController arena;
    public int speed;

    public bool inPool = false;

    int damage;
    // Start is called before the first frame update
    void Awake()
    {
        damage = player.damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPool)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    void PutInPool()
    {
        transform.localPosition = new Vector3(0, 0, 1);
        inPool = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            PutInPool();
        }
        if (col.gameObject.tag == "Enemy")
        {
            var actor = col.gameObject.GetComponent<Actor>();

            actor.TakeDamage(damage);
            PutInPool();
        }
    }
}

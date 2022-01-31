using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArenaController : MonoBehaviour
{
    public float spawnRate;
    public GameObject bomber;
    public GameObject ranger;

    public float chanceToDrop = 0.2f;
    public float chancePowerUp = 0.75f;
    public GameObject powerup;

    SpawnChance spawnChance = new SpawnChance();

    public int xBounds;
    public int yBounds;


    float timeSincePower;
    float timeSinceSpawn;
    public GameObject wall;
        public GameObject star;

    void Awake()
    {
        transform.localScale = new Vector2(xBounds*4, yBounds*4);

        Instantiate(wall, new Vector3(xBounds,0,0),Quaternion.identity, transform).transform.localScale = new Vector3(0.1f,1,1);
        Instantiate(wall, new Vector3(-xBounds,0,0),Quaternion.identity, transform).transform.localScale = new Vector3(0.1f,1,1);
        Instantiate(wall, new Vector3(0,yBounds,0),Quaternion.identity, transform).transform.localScale = new Vector3(1,0.1f,1);
        Instantiate(wall, new Vector3(0,-yBounds,0),Quaternion.identity, transform).transform.localScale = new Vector3(1,0.1f,1);

        for( int ii = 0; ii < 50; ii++)
        {
            Vector3 pos = new Vector3(Random.Range(-xBounds, xBounds), Random.Range(-yBounds, yBounds),2);
            var newStar = Instantiate(star,pos,Quaternion.identity,GameObject.Find("Background").transform);

            var anim = newStar.GetComponent<Animator>();
            anim.speed = Random.Range(0.5f,1.5f);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceSpawn > (1f/spawnRate))
        {
            SpawnyBoi();
            timeSinceSpawn = 0;
        }
        timeSinceSpawn += Time.deltaTime;
    }

    void SpawnyBoi()
    {
        var next = spawnChance.GetNextSpawn();
        bool isX = false;
        if (Random.Range(0,2) == 1)
        {
            isX = true;
        }
        Vector3 point;
        int neg;
        if (Random.Range(0,2) == 1)
        {
            neg = 1;
        } else
        {
            neg = -1;
        }
        if (isX)
        {
            point = new Vector3(neg * (xBounds-2), Random.Range(-yBounds+1, yBounds),0);
        } else
        {
            point = new Vector3(Random.Range(-xBounds+1, xBounds), neg * (yBounds-2),0);
        }

        if (next == EnemyType.Bomber)
        {
            Instantiate(bomber,point,Quaternion.identity);
        } else if (next == EnemyType.Ranged)
        {
            Instantiate(ranger,point,Quaternion.identity);
        }
    }

    public void PowerupSpawn(Vector3 location)
    {
        if (Random.Range(0f,1f) > chanceToDrop)
        {
            return;
        }

        if (Random.Range(0f,1f) < chancePowerUp)
        {
            Instantiate(powerup, location, Quaternion.identity);
        }
    }
}

public class SpawnChance
{
    float[] enemyType = {0.75f, 0.25f};

    public EnemyType GetNextSpawn()
    {
        float total = 0f;
        for (int ii = 0; ii < enemyType.Length; ii++)
        {
            total += enemyType[ii];
        }
        for (int ii = 0; ii < enemyType.Length; ii++)
        {
            enemyType[ii] = enemyType[ii] / total;
        }
        
        var rng = new System.Random();
        var roll = Random.Range(0f,1f);
        var weight = 0f;
        enemyType = enemyType.OrderBy(x => rng.Next()).ToArray();
        for (int ii = 0 ; ii < enemyType.Length; ii++)
        {
            weight += enemyType[ii];
            if (roll < weight)
            {
                return (EnemyType)ii;
            }
        }
        return EnemyType.Bomber;
    }
}

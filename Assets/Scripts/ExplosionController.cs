using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    AudioSource explode;
    public float explosionTime;

    float timeSinceAwake;
    void Awake()
    {
        timeSinceAwake = 0;
        explode = GetComponent<AudioSource>();

    }

    void Start()
    {
        explode.Play();
    }

    void Update()
    {
        timeSinceAwake += Time.deltaTime;

        if (timeSinceAwake >= explosionTime)
        {
            Destroy(gameObject);
        }
    }
}

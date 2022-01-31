using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationController : Actor
{
    void Awake()
    {
        LoadHealth();
    }

        // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }

    public void Reset()
    {
        currentHealth = baseHealth;
    }

}

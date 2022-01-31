using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationController : Actor
{
    public Slider healthSlider;

    new void UpdateHealth()
    {
        healthSlider.value = ((float)currentHealth/baseHealth);
    }
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

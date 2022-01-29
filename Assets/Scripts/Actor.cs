using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int baseHealth;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        baseHealth = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Heal(int health)
    {
        if ((currentHealth + health) <= baseHealth)
        {
            currentHealth += health;
        } else 
        {
            currentHealth = baseHealth;
        }
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }
}

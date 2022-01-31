using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int baseHealth;

    public Transform healthSprite;
    protected int currentHealth;
    // Start is called before the first frame update
    protected void LoadHealth()
    {
        currentHealth = baseHealth;
    }

    protected void UpdateHealth()
    {
        healthSprite.localScale = new Vector3((currentHealth/(float)baseHealth),0.05f, 1);
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
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return true;
        }
        return false;
    }
}

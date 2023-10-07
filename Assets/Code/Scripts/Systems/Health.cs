using System;
using UnityEngine;

public class Health
{
    // Private
    private int maxHealth = 1;
    private int currentHealth;


    // Event
    public event Action<int> Modified;

    public Health(int maxHealth)
    {
        this.maxHealth = maxHealth;
        
    }

    public void Modify(int delta)
    {
        currentHealth = Mathf.Clamp(currentHealth + delta, 0, maxHealth);

        Modified?.Invoke(currentHealth);
    }
}
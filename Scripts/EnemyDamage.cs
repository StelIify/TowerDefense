using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int maxHealth = 300;
    [SerializeField] private int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
        
    }
    public void ModifyHealth()
    {
        currentHealth -= 50;
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }
    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject particle)
    {
        ModifyHealth();
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}

﻿using UnityEngine;

public class Damageable : MonoBehaviour
{
    private float health = 100f;
    private HealthBarUI healthBar;

    public bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    void Start()
    {
        healthBar = transform.Find("HealthBar")?.GetComponent<HealthBarUI>();
    }

    void Update()
    {
        // TODO: For testing only, remove later
        if (Input.GetKeyDown(KeyCode.T))
        {
            ApplyDamage(20);
        }
    }
    private void StartDeath()
    {
        if (this.tag == "Enemy")
        {
            Spawner.enemyList.Remove(this.gameObject);
        }
        GameObject.Destroy(this.gameObject);
    }

    /// <summary>
    /// Subtracts the given value from health and triggers death if health reaches 0
    /// </summary>
    /// <param name="damage">Value to be subtracted</param>
    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartDeath();
            healthBar?.SetHealth(0);
        }
        else
        {
            healthBar?.SetHealth(health);
        }
    }

    public float GetHealth()
    {
        return health;
    }
}

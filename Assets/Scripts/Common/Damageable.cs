using UnityEngine;

/// <summary>
/// Component that implements health.
/// </summary>
public class Damageable : MonoBehaviour
{

    /// <summary>
    /// State of the object.
    /// </summary>
    /// <value>True if dead.</value>
    public bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    private float health = 100f;
    private HealthBarUI healthBar;

    void Start()
    {
        healthBar = transform.Find("HealthBar")?.GetComponent<HealthBarUI>();
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

    /// <summary>
    /// Getter for the health of the object.
    /// </summary>
    /// <returns>A float between 0 and 100.</returns>
    public float GetHealth()
    {
        return health;
    }
}

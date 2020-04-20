using UnityEngine;

public class Damageable : MonoBehaviour
{
    private float health = 100f;

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
        }
    }

    private void StartDeath()
    {
        // 
    }
}

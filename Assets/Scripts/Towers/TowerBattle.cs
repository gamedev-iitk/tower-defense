using UnityEngine;

/// <summary>
/// Component to handle tower attacks
/// </summary>
public class TowerBattle : AbstractBattle
{
    private Damageable enemyDamageable;
    private GameObject target;
    private bool isFighting = false;
    private float timer;

    override protected void SetRange()
    {
        GetComponent<SphereCollider>().radius = Range;
    }

    override public void OnDetect(GameObject enemy)
    {
        // Starts conditions for an attack
        target = enemy;
        enemyDamageable = target.GetComponent<Damageable>();
        isFighting = true;
        timer = 0f;
    }

    override public void OnLose()
    {
        isFighting = false;
        target = null;
        GetComponent<Detection>().Reset();
    }

    void Update()
    {
        // Checks if to attack an enemy
        if (isFighting)
        {
            // Start the timer for FireRate
            timer += Time.deltaTime;
            if (timer >= FireRate)
            {
                enemyDamageable?.ApplyDamage(Attack);

                if (enemyDamageable.IsDead)
                {
                    OnLose();
                }

                // TODO: Decide if we should do raycasts first, when most tower attacks are going to be area-of-effect
                // Vector3 direction = target.transform.position - transform.position;
                // if (Physics.Raycast(transform.position, direction, out RaycastHit hit)) {}

                // Reset the timer
                timer = 0f;
            }
        }
    }
}

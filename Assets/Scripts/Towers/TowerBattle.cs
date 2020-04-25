using UnityEngine;

public class TowerBattle : AbstractBattle
{
    private bool isFighting = false;
    private float timer;
    private Damageable enemyDamageable;
    private GameObject target;

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
                Debug.Log(enemyDamageable.GetHealth());
                enemyDamageable?.ApplyDamage(Attack);

                // TODO: Decide if we should do raycasts first, when most tower attacks are going to be area-of-effect
                // Vector3 direction = target.transform.position - transform.position;
                // if (Physics.Raycast(transform.position, direction, out RaycastHit hit)) {}

                // Reset the timer
                timer = 0f;
            }
        }
    }
}

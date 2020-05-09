using UnityEngine;

/// <summary>
/// Component to handle tower attacks
/// </summary>
public class RangedTowerBattle : AbstractBattle
{
    private GameObject target;
    private bool isFighting = false;
    private bool turn = false;
    private float timer;

    override public void OnDetect(GameObject[] targets)
    {
        // TODO: Select the closest target instead of the first
        target = targets[0];
        isFighting = true;
        turn = true;
        timer = 0f;
    }

    override public void OnLose()
    {
        target = null;
        isFighting = false;
        turn = false;
        GetComponent<Detection>().SetOccupied(false);
    }

    void Update()
    {
        if (target == null) {
            OnLose();
        }

        // Rotate to face the target
        if (turn)
        {
            Vector3 look = target.transform.position - transform.position;
            look.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }


        if (isFighting)
        {
            // Start the timer for FireRate
            timer += Time.deltaTime;
            if (timer >= FireRate)
            {
                Damageable damageable = target.GetComponent<Damageable>();
                damageable.ApplyDamage(Attack);

                float distance = (target.transform.position - transform.position).magnitude;
                if (damageable.IsDead || distance > Range)
                {
                    OnLose();
                }

                // Reset the timer
                timer = 0f;
            }
        }
    }
}

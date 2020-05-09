using UnityEngine;

public class MineTowerBattle : AbstractBattle
{
    private GameObject[] targets;
    private bool isFighting = false;
    private float timer;

    override public void OnDetect(GameObject[] tgt)
    {
        targets = tgt;
        isFighting = true;
        timer = FireRate - 0.5f;
    }

    override public void OnLose()
    {
        targets = null;
        isFighting = false;
        GetComponent<Detection>().SetOccupied(false);
    }

    void Update()
    {
        if (isFighting)
        {
            // Start the timer for FireRate
            timer += Time.deltaTime;
            if (timer >= FireRate)
            {
                int targetsDamaged = 0;
                foreach (GameObject target in targets)
                {
                    if (target != null)
                    {
                        float distance = (target.transform.position - transform.position).magnitude;
                        Damageable dam = target.GetComponent<Damageable>();
                        if (distance < Range && !dam.IsDead)
                        {
                            dam.ApplyDamage(Attack);
                            targetsDamaged++;
                        }
                    }
                }

                // None of the targets can be attacked
                if (targetsDamaged == 0)
                {
                    OnLose();
                }

                // Reset the timer
                timer = 0f;
            }
        }
    }
}

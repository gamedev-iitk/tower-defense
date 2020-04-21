
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public bool isFighting = false;
    public float attack, defence, range, rateOfFire;

    public bool isDamageable = false;
    public float timer;

    public GameObject enemytoTarget;

    void Start()
    {
        //sets range 
        GetComponent<CapsuleCollider>().radius = range;
    }
    public void Attack(GameObject enemy)
    {
        //strarts conditions for an attack
        enemytoTarget = enemy;
        if (enemytoTarget.GetComponent<Damageable>() != null)
        {
            isDamageable = true; ;
        }
        isFighting = true;
        timer = 0f;
    }

    public void stopAttack()
    {
        //stops attack
        isFighting = false;
        enemytoTarget = null;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= rateOfFire)
        {
            //checks if to attack an enemy
            if (isFighting)
            {
                //checks for a clear line of sight
                Vector3 direction = enemytoTarget.transform.position - transform.position;
                if (Physics.Raycast(transform.position, direction, out RaycastHit hit))
                {
                    if (isDamageable)
                    {
                        //damages enemy
                        enemytoTarget.GetComponent<Damageable>().ApplyDamage(attack);
                    }
                }
            }
            timer = 0f;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isDamageable : MonoBehaviour
{
    public float health,defense;

    public bool Damage(float attack)
    {
        float  reducedDamage=attack-(defense/100)*attack;
        Debug.Log("Health : "+health);
        health=health-reducedDamage;
        if(health<=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

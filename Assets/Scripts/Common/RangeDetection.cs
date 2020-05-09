using System.Collections.Generic;
using System.Threading;
using UnityEditor.Graphs;
using UnityEngine;

/// <summary>
/// Component to detect enemies using sweeps of a "detection cone"
/// </summary>
public class RangeDetection : MonoBehaviour
{
    private AbstractBattle battle;
    private float detectionRange;
    private void Start()
    {
        battle = GetComponent<AbstractBattle>();
        detectionRange = battle.Range;
    }

    public void DetectEnemys()
    {
        Collider[] col = Physics.OverlapSphere(gameObject.transform.position, detectionRange, LayerMask.GetMask("Enemy"),
            QueryTriggerInteraction.Collide); // Radius is how big you want the sphere to be, layermask will be in  this case the layer in which enemies/npcs are on

        if (col.Length > 0)
        {
            foreach (Collider hit in col)
            {
                if (hit.tag == "Enemy")
                {
                    if (hit.gameObject.GetComponent<Damageable>() != null)
                    {
                        hit.gameObject.GetComponent<Damageable>().ApplyDamage(battle.Attack);
                    }
                }
            }
        }
    }
}
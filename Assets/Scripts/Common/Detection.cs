using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to detect enemies using Physics.OverlapSphere
/// </summary>
public class Detection : MonoBehaviour
{
    /// <summary>
    /// State of the object.
    /// </summary>
    /// <value>True if the object is attacking, False if it is idle and should detect.</value>
    public bool IsOccupied { get; set; } = false;
    private AbstractBattle battle;

    void Start()
    {
        battle = GetComponent<AbstractBattle>();
    }

    void FixedUpdate()
    {
        if (!IsOccupied)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, battle.Range, LayerMask.GetMask("Enemy"));
            if (colliders.Length > 0)
            {
                // Raycast to check if these targets are in line of sight
                List<GameObject> targets = new List<GameObject>();
                foreach (Collider collider in colliders)
                {
                    Vector3 dir = collider.transform.position - transform.position;
                    if (Physics.Raycast(transform.position, dir, out RaycastHit hit, battle.Range))
                    {
                        if (Equals(hit.collider, collider))
                        {
                            targets.Add(collider.gameObject);
                        }
                    }
                }

                if (targets.Count > 0)
                {
                    Detected(targets.ToArray());
                }
            }
        }
    }

    void Detected(GameObject[] targets)
    {
        IsOccupied = true;
        battle?.OnDetect(targets);
    }
}

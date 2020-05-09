using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to detect enemies using Physics.OverlapSphere
/// </summary>
public class Detection : MonoBehaviour
{
    private bool isOccupied = false;
    private AbstractBattle battle;

    void Start()
    {
        battle = GetComponent<AbstractBattle>();
    }

    void FixedUpdate()
    {
        if (!isOccupied)
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

                Detected(targets.ToArray());
            }
        }
    }

    void Detected(GameObject[] targets)
    {
        isOccupied = true;
        battle?.OnDetect(targets);
    }

    public void SetOccupied(bool val)
    {
        isOccupied = val;
    }

    public bool GetOccupied()
    {
        return isOccupied;
    }
}

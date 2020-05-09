using UnityEngine;
using UnityEngine.AI;

// TODO: Use detection and battle components for enemy AI.
/// <summary>
/// Implementation for the berzerker's AI.
/// </summary>
public class AIBerzerker : MonoBehaviour
{
    private bool isFighting = false;
    private Vector3 destination;
    private NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        destination = GameObject.Find("Shinboku").transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination);
    }

    void FixedUpdate()
    {
        if (!isFighting)
        {
            Vector3 dir = destination - transform.position;
            Ray visionRay = new Ray(transform.position, dir);
            int mask = LayerMask.GetMask(new[] { "Player", "Base", "TowerGeometry" });
            Debug.DrawRay(transform.position, dir * 3f, Color.red, 0.5f);
            if (Physics.Raycast(visionRay, out RaycastHit hit, mask))
            {
                target = hit.transform;
                if (target.CompareTag("Tower") || target.CompareTag("Player") || target.CompareTag("Base"))
                {
                    agent.SetDestination(target.position);
                    isFighting = true;
                    Attack();
                }
            }
            else
            {
                agent.SetDestination(destination);
            }
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("attack");
        }
    }
}

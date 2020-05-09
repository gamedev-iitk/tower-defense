using UnityEngine;
using UnityEngine.AI;

public class AIBerzerker : MonoBehaviour
{
    private Vector3 destination;
    private NavMeshAgent agent;

    Transform target;
    bool isFighting = false;
    void Start()
    {
        destination = GameObject.Find("Shinboku").transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination);
    }

    // TODO: Attack anyone on detect.
    void FixedUpdate()
    {
        if (!isFighting)
        {
            Vector3 dir = destination - transform.position;
            Ray visionRay = new Ray(transform.position, dir);
            int mask = LayerMask.GetMask(new[] {"Player", "Base", "TowerGeometry"});
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

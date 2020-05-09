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
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isFighting)
        {
            Vector3 dir = destination - transform.position;
            Ray vision_ray = new Ray(transform.position, dir);
            int mask = LayerMask.GetMask("Player") | LayerMask.GetMask("Base") | LayerMask.GetMask("TowerGeometry");
            Debug.DrawRay(transform.position, dir * 3f, Color.red, 0.5f);
            if (Physics.Raycast(vision_ray, out RaycastHit hit, mask))
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
            agent.SetDestination(target.position);
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

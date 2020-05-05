using UnityEngine;
using UnityEngine.AI;

public class AIBerzerker : MonoBehaviour
{
    public Transform StartTransform;
    public Transform EndTransform;

    private NavMeshAgent agent;

    Transform target;
    bool isFighting = false;
    void Start()
    {
        StartTransform = transform;
        EndTransform = GameObject.FindWithTag("Base").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(StartTransform.position);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isFighting)
        {
            Vector3 dir = EndTransform.position - transform.position;
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
                agent.SetDestination(EndTransform.position);
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

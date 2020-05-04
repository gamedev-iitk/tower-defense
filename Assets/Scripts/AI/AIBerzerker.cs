using UnityEngine;
using UnityEngine.AI;

public class AIBerzerker : MonoBehaviour
{
    public Transform StartTransform;
    public Transform EndTransform;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(StartTransform.position);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = EndTransform.position - transform.position;
        Ray vision_ray = new Ray(transform.position, dir);
        int mask = LayerMask.GetMask("Enemy");

        if (Physics.Raycast(vision_ray, out RaycastHit hit, mask))
        {
            Transform target = hit.transform;
            if (!target.CompareTag("Enemy"))
            {
                agent.SetDestination(target.position);
                Attack();
            }
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

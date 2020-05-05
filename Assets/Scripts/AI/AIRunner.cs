using UnityEngine;
using UnityEngine.AI;

public class AIRunner : MonoBehaviour
{
    public Transform StartTransform;
    public Transform EndTransform;

    private NavMeshAgent agent;

    void Start()
    {
        StartTransform=transform;
        EndTransform=GameObject.FindWithTag("Base").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(StartTransform.position);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        agent.SetDestination(EndTransform.position);
        Attack();
    }

    private void Attack()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("attack");
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

public class AIRunner : MonoBehaviour
{
    private Vector3 destination;
    private NavMeshAgent agent;

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
        agent.SetDestination(destination);
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

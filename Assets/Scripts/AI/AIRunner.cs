using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Implementation for the runner's AI.
/// </summary>
public class AIRunner : MonoBehaviour
{
    private Vector3 destination;
    private NavMeshAgent agent;

    void Start()
    {
        destination = GameObject.Find("Shinboku").transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination);
    }

    private void Attack()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("attack");
        }
    }
}

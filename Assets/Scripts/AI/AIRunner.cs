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
        agent.SetDestination(destination);
    }

    // TODO: Attack shinboku on detect

    private void Attack()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("attack");
        }
    }
}

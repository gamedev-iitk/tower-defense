using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //private GameObject enemy;
    public Transform[] waypoints;
    private int m_Index=0;
    private NavMeshAgent agent;
    void Start()
    {
        agent =GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            m_Index = m_Index+1 > waypoints.Length-1 ? waypoints.Length-1 : m_Index+1;
            agent.SetDestination(waypoints[m_Index].position);
        }
    }
}

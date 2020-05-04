using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyAI : MonoBehaviour
{
    //private GameObject enemy;
    public Transform start,end;
    public bool smart;
    private Transform enemy;
    private NavMeshAgent agent;
    public bool Move;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Transform>();
        agent.SetDestination(start.position);
        

    }

    void Smart_Move(NavMeshAgent m_agent)
    {
       // m_Index = m_Index + 1 > waypoints.Length - 1 ? waypoints.Length - 1 : m_Index + 1;
        m_agent.SetDestination(end.position);
        Attack(m_agent);
    }

    void FixedUpdate()
    {
        if (Move)
        {
            ToMove(smart, agent);
            Vector3 dir = end.position - enemy.position;
            Debug.DrawRay(enemy.position, dir);
        }
    }
    
    void ToMove(bool m_smart, NavMeshAgent m_agent)
    {
        if (m_smart)
        {
             Smart_Move(m_agent);
        }
        else
        {
            Dumb_Move(m_agent);
        }
    }

    void Dumb_Move(NavMeshAgent m_agent)
    {
        Vector3 dir = end.position - enemy.position;
        Ray vision_ray = new Ray(enemy.position, dir);
        int mask = LayerMask.GetMask("Enemy");

        if (Physics.Raycast(vision_ray, out RaycastHit hit, mask))
        {
            Transform target = hit.transform;
            if (!target.CompareTag("Enemy"))
            {
                m_agent.SetDestination(target.position);
                Attack(m_agent);
            }
        }
    }
    void Attack(NavMeshAgent m_agent)
    {
        if(m_agent.remainingDistance <= m_agent.stoppingDistance)
        {
            Debug.Log("attack");
        }
    }
}


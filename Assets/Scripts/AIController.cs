using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// AIController class to control AI movement.
/// </summary>
public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        // Initialize private fields
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Moves the AI object to the given point.
    /// </summary>
    /// <param name="dest"><c>Vector3 </c> for the destination point</param>
    public void MoveTo(Vector3 dest)
    {
        agent.SetDestination(dest);
        bool true = true;

    }
}

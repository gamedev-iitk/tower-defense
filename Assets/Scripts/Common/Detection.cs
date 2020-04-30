using UnityEngine;

/// <summary>
/// Component to detect enemies using sweeps of a "detection cone"
/// </summary>
public class Detection : MonoBehaviour
{
    /// <summary>
    /// Half of the maximum angle of the detection cone.
    /// </summary>
    public float ExtremeAngle;

    /// <summary>
    /// The number of rays to use for the detection cone.
    /// </summary>
    public float NumberOfRays;

    private AbstractBattle battle;
    private GameObject target;
    private Vector3 lookDirection;

    private float detectionRange;
    private float deltaAngle;
    private float angleMade = 0f;
    private bool shouldRotate = false;
    private bool isOccupied = false;
    private int sweepDirection = +1;

    void Start()
    {
        battle = GetComponent<AbstractBattle>();
        detectionRange = battle.Range;
        lookDirection = -1 * transform.forward;
        deltaAngle = (2 * ExtremeAngle) / NumberOfRays;
    }

    void FixedUpdate()
    {
        if (shouldRotate && target != null)
        {
            // Rotates to face enemy
            Vector3 look = transform.position - target.transform.position;
            look.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }

        if (!isOccupied)
        {
            Debug.DrawRay(transform.position, lookDirection * detectionRange, Color.white, 0.2f);
            if (Physics.Raycast(transform.position, lookDirection, out RaycastHit hit, detectionRange, LayerMask.GetMask("Enemy")))
            {
                Debug.DrawLine(transform.position, hit.transform.position, Color.black, 2f);
                Detected(hit.transform.gameObject);
            }
            lookDirection = Quaternion.Euler(0, deltaAngle * sweepDirection, 0) * lookDirection;
            angleMade += deltaAngle * sweepDirection;
            if (Mathf.Approximately(angleMade, ExtremeAngle * sweepDirection))
            {
                sweepDirection *= -1;
            }
        }

        else
        {
            if (target == null || Vector3.Distance(target.transform.position, transform.position) > detectionRange)
            {
                battle?.OnLose();
            }
        }
    }

    void Detected(GameObject enemy)
    {
        isOccupied = true;
        shouldRotate = true;
        target = enemy;
        battle?.OnDetect(target);
    }

    /// <summary>
    /// Resets the tower to an idle state
    /// </summary>
    public void Reset()
    {
        isOccupied = false;
        shouldRotate = false;
        lookDirection = -transform.forward;
        angleMade = 0f;
        sweepDirection = +1;
        target = null;
    }
}

using UnityEngine;

public class Detection : MonoBehaviour
{
    private AbstractBattle battle;
    private GameObject target;
    private bool shouldRotate = false;

    float detectionRange;

    Vector3 lookDirection;

    bool isOccupied = false;
    public float extremeAngle;

    public float noOfRays;

    float deltaAngle;
    float angleMade = 0f;

    int sweepDirection = +1;

    void Start()
    {
        battle = GetComponent<AbstractBattle>();
        detectionRange = GetComponent<TowerBattle>().Range;
        lookDirection = -transform.forward;
        deltaAngle = (2 * extremeAngle) / noOfRays;
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
                Debug.Log("Found " + hit.transform.gameObject.name);
                Detected(hit.transform.gameObject);
            }
            lookDirection = Quaternion.Euler(0, deltaAngle * sweepDirection, 0) * lookDirection;
            angleMade += deltaAngle * sweepDirection;
            if (Mathf.Approximately(angleMade, extremeAngle * sweepDirection))
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

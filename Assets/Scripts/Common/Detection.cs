using UnityEngine;

public class Detection : MonoBehaviour
{
    private AbstractBattle battle;
    private GameObject target;
    private bool shouldRotate = false;

    void Start()
    {
        battle = GetComponent<AbstractBattle>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = other.gameObject;
            shouldRotate = true;
            battle?.OnDetect(target);
        }
    }

    void OnTriggerExit(Collider other)
    {
        shouldRotate = false;
        battle?.OnLose();
    }

    void Update()
    {
        if (shouldRotate && target != null)
        {
            // Rotates to face enemy
            Vector3 look = transform.position - target.transform.position;
            look.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }
    }
}

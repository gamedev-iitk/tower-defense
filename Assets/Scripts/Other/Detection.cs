using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public Battle battle;

    public GameObject target;
    public bool shouldRotate = false;

    void OnTriggerEnter(Collider other)
    {
        //checks if collider has tag "Enemy"
        if (other.gameObject.tag == "Enemy")
        {
            target = other.gameObject;
            shouldRotate = true;
            battle.Attack(target);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //stops attack if enemy is out of range
        shouldRotate = false;
        battle.stopAttack();
    }

    void Update()
    {
        if (shouldRotate)
        {
            //rotates to face enemy
            Vector3 look = transform.position - target.transform.position;
            look.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }
    }
}

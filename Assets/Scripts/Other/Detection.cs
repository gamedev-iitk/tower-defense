using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public Battle battle;
  public GameObject enemy;

  public GameObject target;
  public bool shouldRotate=false;

  void OnTriggerEnter(Collider other)
  {
      Debug.Log("entered");
      if(other.gameObject.tag=="Enemy")
      {
          Debug.Log("Called for attack");
          target=other.gameObject;
          shouldRotate=true;
          battle.Attack(target);
      }
  }

  void OnTriggerExit(Collider other)
  {
      Debug.Log("exited");
      shouldRotate=false;
      battle.stopAttack();
  }

  void Update()
  {
      if(shouldRotate)
      {
          Vector3 look=transform.position-target.transform.position;
          look.y=0;
          Quaternion targetRotation=Quaternion.LookRotation(look);
          transform.rotation=Quaternion.Lerp(transform.rotation,targetRotation,0.1f);
      }
  }
}

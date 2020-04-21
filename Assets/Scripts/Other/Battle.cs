using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
  public bool isFighting=false;
  public float attack,range,rateOfFire;

  public bool isEnemyDead=true;
  public float timer;

  public GameObject enemytoTarget;

   void Start()
   {
        GetComponent<CapsuleCollider>().radius=range;
   }
  public void Attack(GameObject enemy)
  {
      Debug.Log("Started fighting");
      enemytoTarget=enemy;
      if(enemytoTarget.GetComponent<isDamageable>()!=null)
      {
          Debug.Log("enemy can be damaged");
          isEnemyDead=false;
      }
      isFighting=true;
      timer=0f;
  }

  public void stopAttack()
  {
      Debug.Log("stopped fighting");
      isFighting=false;
      enemytoTarget=null;
  }

  void Update()
  {
      timer+=Time.deltaTime;
      if(timer>=rateOfFire)
      {
         if(isFighting)
        {
          Debug.Log("Attack");
          if(!isEnemyDead)
          {
              isEnemyDead=enemytoTarget.GetComponent<isDamageable>().Damage(attack);
              Debug.Log("damage Done");
              if(isEnemyDead)
              {
                Debug.Log("enemy is dead");
                GameObject.Destroy(enemytoTarget);
                stopAttack();
                this.GetComponent<Detection>().shouldRotate=false;
                Debug.Log("attack has been stopped");
              }
          }  
        }
        timer=0f;
      }
  }

}

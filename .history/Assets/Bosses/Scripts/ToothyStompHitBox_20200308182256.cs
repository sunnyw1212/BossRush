using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyStompHitBox : EnemyHitBox
{


  override public Vector3 GetDirection(Rigidbody otherRigidBody)
  {
    Vector3 direction = otherRigidBody.transform.position - transform.position;
    direction.y = 50f;
    return direction;
  }


  // private static readonly Dictionary<string, int> m_State = new Dictionary<string, int> {
  //   {"Idle", 0},
  //   {"Intro", 1},
  //   {"Chase", 2},
  //   {"Combat", 3},
  // };


  // // Update is called once per frame
  // private void Update()
  // {
  //   var distanceFromPlayer = GetDistanceFromPlayer();
  //   Debug.Log("Distance " + m_AttackRange);

  //   if (distanceFromPlayer <= m_AttackRange)
  //   {
  //     Debug.Log("attack " + m_State["Combat"]);

  //     // attack
  //     m_Animator.SetInteger("state", m_State["Combat"]);
  //   }
  //   else
  //   {
  //     // Debug.Log("chase " + distanceFromPlayer);
  //     //chase 
  //     m_Animator.SetInteger("state", m_State["Chase"]);
  //   }
  // }

}

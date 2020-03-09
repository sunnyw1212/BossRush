using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyAIBossController : MonoBehaviour
{
  [SerializeField] float m_AttackRange = 3f;

  private static readonly Dictionary<string, float> m_State = new Dictionary<string, float> {
    {"Idle", 0},
    {"Intro", 1},
    {"Chase", 2},
    {"Combat", 3},
  };


  // Update is called once per frame
  private void Update()
  {
    var distanceFromPlayer = GetDistanceFromPlayer();

    if (distanceFromPlayer <= m_AttackRange)
    {
      // attack
      m_Animator.SetFloat("state", m_State["Combat"]);
    }
    else
    {
      //chase 
      m_Animator.SetFloat("state", m_State["Chase"]);
    }
  }

}

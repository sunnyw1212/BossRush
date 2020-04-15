﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyStomp : BaseBossStateMachineBehaviour
{

  private ToothyAIBossController m_ToothyAIBossController;
  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateEnter(animator, stateInfo, layerIndex);
    m_ToothyAIBossController = m_AIBossController.GetComponent<ToothyAIBossController>();
    Vector3 playerPosition = m_Player.transform.position;
    playerPosition.y = 0f;
    m_ToothyAIBossController.transform.LookAt(playerPosition);
    // m_ToothyAIBossController.ToggleHitboxColliders("ToothyStomp", true);
    // m_ToothyAIBossController.RotateTowards(m_Player.transform);
    // m_ToothyAIBossController.transform.LookAt(m_Player.transform);
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateUpdate(animator, stateInfo, layerIndex);
    // m_ToothyAIBossController.RotateTowards(m_Player.transform);

  }

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    // m_ToothyAIBossController.ToggleHitboxColliders("ToothyStomp", false);
  }

  // OnStateMove is called right after Animator.OnAnimatorMove()
  //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    // Implement code that processes and affects root motion
  //}

  // OnStateIK is called right after Animator.OnAnimatorIK()
  //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  //{
  //    // Implement code that sets up animation IK (inverse kinematics)
  //}
}
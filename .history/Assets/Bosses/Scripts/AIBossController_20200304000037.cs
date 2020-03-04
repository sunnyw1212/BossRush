﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossController : MonoBehaviour
{
  [SerializeField] private GameObject m_Player;
  [SerializeField] float m_MovingTurnSpeed = 360;
  [SerializeField] float m_StationaryTurnSpeed = 180;
  [SerializeField] float m_initialHealth = 100;
  private float m_TurnAmount;
  private float m_ForwardAmount;
  private Animator m_Animator;

  public GameObject GetPlayer()
  {
    return m_Player;
  }

  public void Move(Vector3 move)
  {

    // convert the world relative moveInput vector into a local-relative
    // turn amount and forward amount required to head in the desired
    // direction.
    if (move.magnitude > 1f) move.Normalize();
    move = transform.InverseTransformDirection(move);
    // CheckGroundStatus();
    move = Vector3.ProjectOnPlane(move, Vector3.up);
    m_TurnAmount = Mathf.Atan2(move.x, move.z);
    m_ForwardAmount = move.z;

    ApplyExtraTurnRotation();

    // // control and velocity handling is different when grounded and airborne:
    // if (m_IsGrounded)
    // {
    //   HandleGroundedMovement(crouch, jump);
    // }

    // send input and other state parameters to the animator
    UpdateLocomotionAnimator(move);
  }

  private void ApplyExtraTurnRotation()
  {
    // help the character turn faster (this is in addition to root rotation in the animation)
    float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
    transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
  }

  // set animator params in here
  private void UpdateLocomotionAnimator(Vector3 move)
  {
    // update the animator parameters
    m_Animator.SetFloat("horizontal", m_TurnAmount);
    m_Animator.SetFloat("vertical", m_ForwardAmount);
  }

  void Awake()
  {
    m_Animator = GetComponent<Animator>();
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}

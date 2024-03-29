﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Block : MonoBehaviour
{
  [SerializeField] private Damage m_Damage;

  [SerializeField] private float m_MaxChargeDuration = 5f;

  [SerializeField] private float m_HitTimeScaleSlowdownDuration = 1f;

  [SerializeField] private float m_YKnockbackForceOverride = 3f;

  [SerializeField] private float m_TimeScaleSlowdown = .4f;

  private float m_CurrentChargeDuration;
  private Animator m_Animator;

  private BaseHitBox m_BaseHitBox;

  private PlayerController m_PlayerController;

  IEnumerator EndUppercutAttack()
  {
    yield return new WaitForSecondsRealtime(0.5f);
    m_PlayerController.ToggleHitboxColliders("UppercutAttack", false);
    m_CurrentChargeDuration = 0f;

  }

  IEnumerator ResetTimeScale()
  {
    yield return new WaitForSecondsRealtime(m_HitTimeScaleSlowdownDuration);
    if (Time.timeScale < 1f)
    {
      Time.timeScale = 1f;
    }
  }

  // Start is called before the first frame update
  private void Awake()
  {
    m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    m_Animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    // m_BaseHitBox = gameObject.GetComponent<BaseHitBox>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButton("Fire2"))
    {
      m_PlayerController.ToggleHitboxColliders("UppercutAttack", false);

      if (!m_Animator.GetBool("isChargingUppercutAttack"))
      {
        m_Animator.SetBool("canSwitchWeapon", false);
        m_Animator.SetBool("isChargingUppercutAttack", true);
      }
      m_CurrentChargeDuration += Time.deltaTime;
      Debug.Log("CURRENTCHARGET" + m_CurrentChargeDuration);
    }
    if (Input.GetButtonUp("Fire2") || m_CurrentChargeDuration >= m_MaxChargeDuration)
    {
      m_Animator.SetBool("isChargingUppercutAttack", false);
      m_Animator.SetTrigger("uppercutAttack");
      m_Animator.SetBool("canSwitchWeapon", true);
      m_CurrentChargeDuration = 0f;
      // m_PlayerController.ToggleHitboxColliders("UppercutAttack", true);
      // StartCoroutine(EndUppercutAttack());

    }
  }

  private void OnTriggerEnter(Collider otherCollider)
  {
    Debug.Log("YO YOU HIT SOMETHING WITH UPPERCUT ATTACK" + otherCollider);
    if (otherCollider.gameObject.tag == "Enemy")
    {
      Rigidbody enemyRigidBody = otherCollider.gameObject.GetComponent<Rigidbody>();
      Target enemyTarget = otherCollider.gameObject.GetComponent<Target>();
      NavMeshAgent enemyNavMeshAgent = otherCollider.gameObject.GetComponent<NavMeshAgent>();
      Animator enemyAnimator = otherCollider.gameObject.GetComponent<Animator>();

      Vector3 direction = m_BaseHitBox.GetDirection(enemyRigidBody);
      float force = m_Damage.m_KnockbackForce;
      direction.y = Mathf.Floor(m_YKnockbackForceOverride * m_CurrentChargeDuration);

      enemyNavMeshAgent.enabled = false;

      Time.timeScale = Mathf.Clamp(1 / (m_TimeScaleSlowdown * m_CurrentChargeDuration), .4f, 1);
      Debug.Log("TIMESCALE" + Time.timeScale);

      StartCoroutine(ResetTimeScale());

      enemyRigidBody.AddForce(direction * force, ForceMode.Impulse);
      var originalDamageAmount = m_Damage.m_DamageAmount;
      m_Damage.m_DamageAmount *= m_CurrentChargeDuration;
      enemyTarget.TakeDamage(m_Damage);
      m_Damage.m_DamageAmount = originalDamageAmount;
    }
  }
}

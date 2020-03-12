using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothyAIBossController : AIBossController {
  [SerializeField] float m_AttackRange = 2f;

  private static readonly Dictionary<string, int> m_State = new Dictionary<string, int> { { "Idle", 0 },
    { "Intro", 1 },
    { "Chase", 2 },
    { "Combat", 3 },
  };

  public void ToggleToothyStompHitboxColliders (bool isEnabled) {
    ToothyStompHitBox[] hitboxes = GetComponentsInChildren<ToothyStompHitBox> ();
    Debug.Log ("hitboxes" + hitboxes);
    for (int i = 0; i < hitboxes.Length; i++) {
      hitboxes[i].m_Collider.enabled = isEnabled;
    }
  }

  public void ToggleToothySwipeHitboxColliders (bool isEnabled) {
    ToothySwipeHitBox[] hitboxes = GetComponentsInChildren<ToothySwipeHitBox> ();
    Debug.Log ("toggle swipe hitboxes" + hitboxes);
    for (int i = 0; i < hitboxes.Length; i++) {
      hitboxes[i].m_Collider.enabled = isEnabled;
    }
  }

  public void ToggleToothyHitboxColliders (string name, bool isEnabled) {
    BaseEnemyHitBox[] hitboxes = GetComponentsInChildren<BaseEnemyHitBox> ();
    Debug.Log ("toggle swipe hitboxes" + hitboxes);
    for (int i = 0; i < hitboxes.Length; i++) {
      if (name == hitboxes.name) {
        hitboxes[i].m_Collider.enabled = isEnabled;
      }
    }
  }

  // Update is called once per frame
  private void Update () {
    var distanceFromPlayer = GetDistanceFromPlayer ();
    Debug.Log ("Distance " + m_AttackRange);

    if (distanceFromPlayer <= m_AttackRange) {
      Debug.Log ("attack " + m_State["Combat"]);

      // attack
      m_Animator.SetInteger ("state", m_State["Combat"]);
    } else {
      // Debug.Log("chase " + distanceFromPlayer);
      //chase 
      m_Animator.SetInteger ("state", m_State["Chase"]);
    }
  }

}
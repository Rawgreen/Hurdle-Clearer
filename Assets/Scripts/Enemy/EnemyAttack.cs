using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyAttack : MonoBehaviour
    {   
        public Transform attackPoint;
        public GameObject ccAttackPrefab;
        private float attackDelay;
        private float attackRange;
        private float timer;
        private Enemy.Stats stats;
        private GameObject targetPlayer;
        private Transform targetPosition;

        void Start() {
            stats = GetComponent<Enemy.Stats>();
            attackDelay = stats.GetAttackDelay();
            attackRange = stats.GetAttackRange();
        }

        void Update() {
            targetPlayer = GameObject.FindGameObjectWithTag("Player");
            targetPosition = targetPlayer.GetComponent<Transform>();

            if(CalculateDistance(targetPosition, gameObject) <= attackRange) {
                timer += Time.deltaTime;
                if (timer >= attackDelay) {
                    PerformAttack();
                    timer = 0f;
                }
            }
        }

        private void PerformAttack() {
            Debug.Log("Attack Performed by " + gameObject.name);
        }

        private float CalculateDistance(Transform target, GameObject currentObject) {
            return (target.transform.position - currentObject.transform.position).magnitude;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}


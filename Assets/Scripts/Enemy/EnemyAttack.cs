using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy {
    public class EnemyAttack : MonoBehaviour
    {   
        [SerializeField]
        private float attackDamage = 25f;
        [SerializeField]
        private float attackRange = 1.5f;
        [SerializeField]
        private float attackDelay = 0.5f;

        public Transform attackPoint;
        public GameObject ccAttackPrefab;
        public LayerMask playerLayer;

        private float timer;
        private GameObject targetPlayer;
        private Transform targetPosition;

        void Update() {
            // Find player and get its position
            targetPlayer = GameObject.FindGameObjectWithTag("Player");
            if (targetPlayer != null) {
                targetPosition = targetPlayer.GetComponent<Transform>();
            }

            // Calculate distance and start attack timer and if timer value more than delay time then perform attack.
            if(CalculateDistance(targetPosition, gameObject) <= attackRange) {
                timer += Time.deltaTime;
                if (timer >= attackDelay) {
                    PerformAttack();
                    timer = 0f;
                }
            }
        }

        private void PerformAttack() {
            // detect player in range of attack
            Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
            
            //deal damage
            if (hitPlayer != null) {
                hitPlayer.GetComponent<Player.PlayerTakeDamage>().Damage(attackDamage);
                Debug.Log(gameObject.name + " hit the target: " + hitPlayer.name);
            }
        }

        // Vector calculation of distance between target position and gameObject position.
        private float CalculateDistance(Transform target, GameObject currentObject) {
            return (target.transform.position - currentObject.transform.position).magnitude;
        }

        // Developer only
        // draw attack distance
        void OnDrawGizmosSelected() {
            if (attackPoint == null) {
                return;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}


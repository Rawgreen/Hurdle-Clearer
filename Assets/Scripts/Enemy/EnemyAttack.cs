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

        public bool drawGizmos = false;
        public Transform attackPoint;
        public GameObject ccAttackPrefab;
        public LayerMask playerLayer;

        private float timer;
        private float attackPointAngle;
        private Vector2 attackPointDirection;
        private GameObject targetPlayer;
        private Transform targetPosition;

        void Update() {
            FindPlayer();

            if (targetPlayer != null) {
                UpdateAttackPointDirection();
            }

            if (targetPosition != null && CalculateDistance(targetPosition, gameObject) <= attackRange) {
                timer += Time.deltaTime;
                if (timer >= attackDelay) {
                    PerformAttack();
                    timer = 0f;
                }
            }
        }

        private void FindPlayer() {
            targetPlayer = GameObject.FindGameObjectWithTag("Player");
            if (targetPlayer != null) {
                targetPosition = targetPlayer.transform;
            } else {
                targetPosition = null;
            }
        }

        private void UpdateAttackPointDirection() {
            attackPointDirection = targetPlayer.transform.position - transform.position;
            attackPointAngle = Mathf.Atan2(attackPointDirection.y, attackPointDirection.x) * Mathf.Rad2Deg;
            attackPoint.rotation = Quaternion.Euler(new Vector3(0, 0, attackPointAngle));
        }

        private void PerformAttack() {
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            foreach (Collider2D player in hitPlayers) {
                Player.PlayerTakeDamage playerDamage = player.GetComponent<Player.PlayerTakeDamage>();
                if (playerDamage != null) {
                    playerDamage.Damage(attackDamage);
                    Debug.Log(gameObject.name + " hit the target: " + player.name);
                }
            }
        }

        private float CalculateDistance(Transform target, GameObject currentObject) {
            if (target == null) {
                Debug.LogError("Target transform is null");
                return 0f;
            }
            if (currentObject == null) {
                Debug.LogError("Current object is null");
                return 0f;
            }
            return (target.transform.position - currentObject.transform.position).magnitude;
        }

        void OnDrawGizmosSelected() {
            if (attackPoint == null) {
                return;
            }
            if (drawGizmos) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(attackPoint.position, attackRange - 1.225f);
            }
        }
    }
}

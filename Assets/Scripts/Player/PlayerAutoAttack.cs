using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Player {
    public class PlayerAutoAttack : MonoBehaviour
    {
        [SerializeField] private bool drawGizmos = false;
        [SerializeField] private float fireRate = 0.5f;
        [SerializeField] private float attackRange = 5f;
        [SerializeField] private Transform projectileSpawnPos;
        [SerializeField] private GameObject projectileInstance;

        private Animator animator;
        private GameObject targetEnemy;
        private Vector2 previousPosition;
        private Rigidbody2D rb;
        private bool canShoot = true;

        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            animator = gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            targetEnemy = FindClosestEnemy();
            if (targetEnemy != null && !IsMoving())
            {
                // Calculate the direction between the player and the target enemy
                Vector2 direction = CalculateDirection(transform.position, targetEnemy.transform.position);
                // Calculate the angle between the player and the target enemy and rotate projectile towards enemy.
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                projectileSpawnPos.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                if (InRange(targetEnemy.transform, attackRange) && canShoot)
                {
                    animator.SetBool("isAttacking", true);
                    StartCoroutine(AttackDelay());
                    canShoot = false;
                }
            }
            else
            {
                //TODO: Remove after testing.
                Debug.LogError("No enemy found within range.");
                return;
            }
        }

        private bool InRange(Transform targetEnemy, float attackRange)
        {
            return Vector2.Distance(transform.position, targetEnemy.transform.position) <= attackRange;
        }

        private bool IsMoving()
        {
            return rb.velocity != Vector2.zero;
        }

        private Vector2 CalculateDirection(Vector2 source, Vector2 target)
        {
            return (target - source).normalized;
        }

        private GameObject FindClosestEnemy()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));

            GameObject closestEnemy = null;
            float closestDistanceSqr = Mathf.Infinity;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float distanceSqr = (collider.transform.position - transform.position).sqrMagnitude;
                    if (distanceSqr < closestDistanceSqr)
                    {
                        closestDistanceSqr = distanceSqr;
                        closestEnemy = collider.gameObject;
                    }
                }
            }
            return closestEnemy;
        }

        public void InstantiateProjectile()
        {
            Instantiate(projectileInstance, projectileSpawnPos.position, projectileSpawnPos.rotation);

        }

        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(fireRate);
            animator.SetBool("isAttacking", false);
            canShoot = true;
        }

        private void OnDrawGizmosSelected()
        {
            if (drawGizmos)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, attackRange);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Player {
    public class PlayerAutoAttack : MonoBehaviour
    {
        [SerializeField] private bool drawGizmos = false;
        [SerializeField] private float fireRate = 0.4f;
        [SerializeField] private float attackRange = 5f;
        [SerializeField] private Transform projectileSpawnPos;
        [SerializeField] private GameObject projectileInstance;

        private GameObject targetEnemy;
        private Vector2 previousPosition;
        private Rigidbody2D rb;
        private bool canShoot = true;

        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            targetEnemy = FindClosestEnemy();

            if (IsMoving() || targetEnemy == null)
            {
                // Reset rotation if the player is moving or the target enemy is destroyed
                transform.rotation = Quaternion.identity;
                projectileSpawnPos.rotation = Quaternion.identity;
                return;
            }

            if (targetEnemy != null)
            {
                // Calculate the direction between the player and the target enemy
                Vector2 direction = CalculateDirection(transform.position, targetEnemy.transform.position);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Rotate the GameObject to face the target enemy
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                // Rotate the projectile spawn position to match the direction
                projectileSpawnPos.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                if (InRange(targetEnemy.transform, attackRange) && canShoot)
                {
                    // Instantiate projectile
                    Instantiate(projectileInstance, projectileSpawnPos.position, projectileSpawnPos.rotation);
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

        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(fireRate);
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

using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Player {
    public class PlayerAutoAttack : MonoBehaviour
    {
        [SerializeField] private float fireRate = 0.4f;
        [SerializeField] private float attackRange = 5f;
        [SerializeField] private Transform playerWeaponPos;
        [SerializeField] private Transform projectileSpawnPos;
        [SerializeField] private GameObject projectileInstance;

        private Transform targetEnemy;

        private float maxDistance = 1000f;
        private bool canShoot = true;

        private void Update()
        {
            targetEnemy = FindClosestEnemy();
            if (targetEnemy != null)
            {
                Vector2 distance = targetEnemy.position - gameObject.transform.position;
                float rotation = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
                playerWeaponPos.transform.rotation = Quaternion.Euler(0f, 0f, rotation);

                if (InRange(targetEnemy.transform, attackRange))
                {
                    if (canShoot)
                    {
                        // Calculate a safe spawn position for the projectile
                        Vector2 spawnPosition = (Vector2)projectileSpawnPos.position + distance.normalized * 1f; // Adjust 0.5f as needed
                        Instantiate(projectileInstance, spawnPosition, projectileSpawnPos.transform.rotation);
                        StartCoroutine(AttackDelay());
                        canShoot = false;
                    }
                }
            }
        }

        private bool InRange(Transform targetEnemy, float attackRange)
        {
            if (Vector2.Distance(gameObject.transform.position, targetEnemy.transform.position) <= attackRange)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        private Transform FindClosestEnemy()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, maxDistance, LayerMask.GetMask("Enemy"));
            Transform closestEnemy = null;
            float closestDistanceSqr = 1000f;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float distanceSqr = (collider.transform.position - transform.position).sqrMagnitude;
                    if (distanceSqr < maxDistance)
                    {
                        closestDistanceSqr = distanceSqr;
                        closestEnemy = collider.transform;
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
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

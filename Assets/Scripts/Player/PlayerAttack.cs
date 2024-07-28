using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Projectile;
using Interfaces;

namespace Player {
    /// <summary>
    /// Manages the player's attack behavior, including projectile firing and attack distance.
    /// </summary>
    public class PlayerAttack : MonoBehaviour, IAttack
    {
        [SerializeField]
        private GameObject projectilePrefab;  // The prefab used for projectiles.

        [SerializeField]
        private GameObject projectileLaunchPosition;  // The position from which projectiles are launched.

        private PlayerStats playerStats;  // Reference to the player's stats.
        private PlayerMovement playerMovement;

        private bool isMoving;
        private float attackDistance;  // The maximum distance at which the player can attack.
        private float attackDelay;  // The delay between consecutive attacks.
        private GameObject enemyObject;  // The enemy object targeted by the player.
        private float enemyDistance;  // The distance between the player and the enemy.
        private float delay;  // The time elapsed since the last attack.

        void Start()
        {
            playerStats = GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                attackDistance = playerStats.GetAttackDistance();
                attackDelay = playerStats.GetAttackDelay();
            }
        }

        void Update() {
            playerMovement = GetComponent<PlayerMovement>();
            if(playerMovement != null) {
                isMoving = playerMovement.isPlayerOnMove();
            }

            enemyObject = GameObject.FindGameObjectWithTag("Enemy");
            if (enemyObject != null)
            {
                // performing vector calculation.
                enemyDistance = (enemyObject.transform.position - transform.position).magnitude;
                if (enemyDistance <= attackDistance && !isMoving) {
                    delay += Time.deltaTime;
                    if (delay >= attackDelay) {
                        PerformAttack();
                        delay = 0f;
                    }
                }
            }
        }

        public void PerformAttack()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileLaunchPosition.transform.position, Quaternion.identity);
            FireProjectile fireProjectile = projectile.GetComponent<FireProjectile>();
            if(fireProjectile != null) {
                fireProjectile.LaunchProjectile(enemyObject.transform.position);
            }
        }

        /* void OnDrawGizmos()
        {
            if (drawAttackDistance)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, initialAttackDistance);
            }
        } */
    }
}

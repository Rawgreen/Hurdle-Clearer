using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Projectile;

namespace Player {
    /// <summary>
    /// Manages the player's attack behavior, including projectile firing and attack distance.
    /// </summary>
    public class PlayerAttack : MonoBehaviour
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

        /// <summary>
        /// Initializes the player's attack settings based on the player's stats.
        /// </summary>
        /// <remarks>
        /// This method is called before the first frame update. It retrieves the attack distance and
        /// attack delay from the PlayerStats component.
        /// </remarks>
        void Start()
        {
            playerStats = GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                attackDistance = playerStats.GetAttackDistance();
                attackDelay = playerStats.GetAttackDelay();
            }
        }

        /// <summary>
        /// Checks the distance to the nearest enemy and handles attacking if within range.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame. It finds the enemy, calculates the distance to
        /// the player, and fires a projectile if the enemy is within attack range and the delay
        /// between attacks has passed.
        /// </remarks>
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
                        FireProjectile();
                        delay = 0f;
                    }
                }
            }
        }

        /// <summary>
        /// Fires a projectile towards the current target enemy.
        /// </summary>
        /// <remarks>
        /// This method instantiates a projectile at the specified launch position and directs it
        /// towards the enemy's position.
        /// </remarks>
        void FireProjectile()
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

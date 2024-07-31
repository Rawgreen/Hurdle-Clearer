using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Projectile;
using Interfaces;

namespace Player {
    public class PlayerAttack : MonoBehaviour, IAttack
    {
        [SerializeField]
        private GameObject projectilePrefab;  

        [SerializeField]
        private GameObject projectileLaunchPosition;  

        private PlayerStats playerStats;  
        private PlayerMovement playerMovement;
        private Animator animator;

        private bool isMoving;
        private float attackDistance;  
        private float attackDelay;  
        private GameObject enemyObject;  
        private float enemyDistance;  
        private float delay;

        void Start()
        {
            playerStats = GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                attackDistance = playerStats.GetAttackDistance();
                attackDelay = playerStats.GetAttackDelay();
            }
            animator = GetComponent<Animator>();
            if (animator == null) {
                Debug.LogError("Animator Couldn't found");
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
                animator.SetBool("isAttacking", true);
            }
        }

        // This method will be called by an Animation Event at the end of the Attack animation
        public void ResetAttack()
        {
            animator.SetBool("isAttacking", false);
        }

        /* void OnDrawGizmosSelected()
        {
            if (drawAttackDistance)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, initialAttackDistance);
            }
        } */
    }
}

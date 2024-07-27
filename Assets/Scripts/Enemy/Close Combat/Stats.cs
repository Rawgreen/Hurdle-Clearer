using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    /// <summary>
    /// Stores and provides access to the stats of an enemy character.
    /// </summary>
    public class Stats : MonoBehaviour
    {
        [SerializeField]
        private float enemyHealth = 30f;  // The health of the enemy.
        
        [SerializeField]
        private float moveSpeed = 1.5f;  // The movement speed of the enemy.
        
        [SerializeField]
        private float attackRange = 1.5f;

        [SerializeField]
        private float attackDelay = 1f;

        /// <summary>
        /// Gets the enemy's current health.
        /// </summary>
        /// <returns>
        /// The current health of the enemy.
        /// </returns>
        public float GetEnemyHealth() {
            return enemyHealth;
        }

        /// <summary>
        /// Gets the enemy's movement speed.
        /// </summary>
        /// <returns>
        /// The movement speed of the enemy.
        /// </returns>
        public float GetMoveSpeed() {
            return moveSpeed;
        }

        public float GetAttackRange() {
            return attackRange;
        }

        public float GetAttackDelay() {
            return attackDelay;
        }
    }
}

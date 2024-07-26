using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    /// <summary>
    /// Stores and provides access to the player's stats.
    /// </summary>
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        private float playerHealth = 100f;  // The health of the player.

        [SerializeField]
        private float moveSpeed = 5f;  // The movement speed of the player.

        [SerializeField]
        private float attackDelay = 1f;  // The delay between consecutive attacks.

        [SerializeField]
        private float attackDistance = 5f;  // The maximum distance at which the player can attack.

        /// <summary>
        /// Gets the player's current health.
        /// </summary>
        /// <returns>
        /// The current health of the player.
        /// </returns>
        public float GetPlayerHealth() {
            return playerHealth;
        }

        /// <summary>
        /// Gets the player's movement speed.
        /// </summary>
        /// <returns>
        /// The movement speed of the player.
        /// </returns>
        public float GetMoveSpeed() {
            return moveSpeed;
        }

        /// <summary>
        /// Gets the delay between consecutive attacks.
        /// </summary>
        /// <returns>
        /// The attack delay of the player.
        /// </returns>
        public float GetAttackDelay() {
            return attackDelay;
        }

        /// <summary>
        /// Gets the maximum distance at which the player can attack.
        /// </summary>
        /// <returns>
        /// The attack distance of the player.
        /// </returns>
        public float GetAttackDistance() {
            return attackDistance;
        }
    }   
}

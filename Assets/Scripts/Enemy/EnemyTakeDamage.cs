using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    /// <summary>
    /// Handles the damage and death of an enemy character.
    /// </summary>
    public class EnemyTakeDamage : MonoBehaviour
    {
        public HealthBar healthBar;  // Reference to the health bar UI element.
        public float currentHealth;  // The current health of the enemy.

        private Stats stats;  // Reference to the enemy's stats.

        /// <summary>
        /// Initializes the enemy's health and sets up the health bar.
        /// </summary>
        /// <remarks>
        /// This method retrieves the enemy's initial health from the Stats component and
        /// sets the maximum health value for the health bar.
        /// </remarks>
        void Start() {
            stats = GetComponent<Stats>();
            currentHealth = stats.GetEnemyHealth();
            healthBar.SetMaxHealth(currentHealth);
        }

        /// <summary>
        /// Checks if the enemy's health has dropped to zero or below and handles death.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame to check the enemy's health and
        /// trigger the death process if necessary.
        /// </remarks>
        void Update() {
            if (currentHealth <= 0) {
                Die();
            }
        }

        /// <summary>
        /// Reduces the enemy's health by a specified amount and updates the health bar.
        /// </summary>
        /// <param name="damage">
        /// The amount of damage to apply to the enemy.
        /// </param>
        public void TakeDamage(float damage) {
            currentHealth -= damage;
            healthBar.SetHealthValue(currentHealth);
        }

        /// <summary>
        /// Handles the enemy's death, including logging a message and destroying the game object.
        /// </summary>
        /// <remarks>
        /// This method is called when the enemy's health reaches zero or below, indicating
        /// that the enemy has died.
        /// </remarks>
        void Die() {
            Debug.Log(gameObject.name + " died");
            Destroy(gameObject);
        }
    }
}

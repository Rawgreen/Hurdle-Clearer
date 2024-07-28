using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Player {
    public class PlayerTakeDamage : MonoBehaviour, IDamageable {
        
        public float currentHealth;

        public HealthBar healthBar;
        private Player.PlayerStats playerStats;

        void Start() {
            playerStats = gameObject.GetComponent<Player.PlayerStats>();
            currentHealth = playerStats.GetPlayerHealth();
            healthBar.SetMaxHealth(currentHealth);
        }

        void Update() {
            if (currentHealth <= 0) {
                Die();
            }
        }

        public void Damage(float damage) {
            currentHealth -= damage;
            healthBar.SetHealthValue(currentHealth);
        }

        public void Die() {
            Debug.Log(gameObject.name + " died.");
            gameObject.SetActive(false);
        }
    }   
}


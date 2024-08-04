using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Player {
    public class PlayerTakeDamage : MonoBehaviour, IDamageable {
        
        public float playerHealth = 2000f;

        public HealthBar healthBar;

        void Start() {
            healthBar.SetMaxHealth(playerHealth);
        }

        void Update() {
            if (playerHealth <= 0) {
                Die();
            }
        }

        public void Damage(float damage) {
            playerHealth -= damage;
            healthBar.SetHealthValue(playerHealth);
        }

        public void Die() {
            Debug.Log(gameObject.name + " died.");
            gameObject.SetActive(false);
        }
    }   
}


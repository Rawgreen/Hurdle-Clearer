using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyTakeDamage : MonoBehaviour, IDamageable {
        public HealthBar healthBar;  
        public float currentHealth;  

        private Stats stats;  

        void Start() {
            stats = GetComponent<Stats>();
            currentHealth = stats.GetEnemyHealth();
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
            Debug.Log(gameObject.name + " died");
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class EnemyTakeDamage : MonoBehaviour, IDamageable
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private float currentHealth;
        [SerializeField] private float respawnTimer = 3f;

        private Stats stats;
        private Vector2 initialPosition;

        void Start()
        {
            stats = GetComponent<Stats>();
            currentHealth = stats.GetEnemyHealth();
            healthBar.SetMaxHealth(currentHealth);
            initialPosition = transform.position;
        }

        public void Damage(float damage)
        {
            currentHealth -= damage;
            healthBar.SetHealthValue(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            gameObject.SetActive(false);
            GameManager.Instance.StartRespawnCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSecondsRealtime(respawnTimer);
            transform.position = initialPosition;
            currentHealth = stats.GetEnemyHealth();
            healthBar.SetMaxHealth(currentHealth);
            healthBar.SetHealthValue(currentHealth);
            gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Projectile {
    /// <summary>
    /// Manages the behavior of a projectile, including its movement, rotation, and collision.
    /// </summary>
    public class FireProjectile : MonoBehaviour
    {
        public Rigidbody2D rigidBody;  // The Rigidbody2D component used for projectile movement.
        public float destroyDelayTime = 5f;  // The time after which the projectile will be destroyed if it doesn't hit anything.
        public float projectileSpeed = 50f;  // The speed at which the projectile travels.
        public float projectileDamage = 50f;  // The damage dealt by the projectile.

        private float time;  // Tracks the time since the projectile was created.

        /// <summary>
        /// Initializes the Rigidbody2D component if not already assigned.
        /// </summary>
        /// <remarks>
        /// This method is called when the script is first run to ensure the Rigidbody2D
        /// component is set up for the projectile.
        /// </remarks>
        void Start() {
            if (rigidBody == null) {
                rigidBody = GetComponent<Rigidbody2D>();
            }
        }  

        /// <summary>
        /// Updates the projectile's lifetime and destroys it if necessary.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame to check if the projectile's lifetime has
        /// exceeded the destroy delay time, in which case the projectile is destroyed.
        /// </remarks>
        void Update() {
            time += Time.deltaTime;
            if(time >= destroyDelayTime) {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Launches the projectile towards a specified target position.
        /// </summary>
        /// <param name="targetPosition">
        /// The position towards which the projectile should be launched.
        /// </param>
        /// <remarks>
        /// This method sets the projectile's direction and speed based on the target position,
        /// and rotates the projectile to face the direction of travel.
        /// </remarks>
        public void LaunchProjectile(Vector2 targetPosition) {
            if (rigidBody != null) {
                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
                float projectileRotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, projectileRotation + 90);
                rigidBody.velocity = direction * projectileSpeed;
            }
        }

        /// <summary>
        /// Handles collisions with other objects and applies damage if hitting an enemy.
        /// </summary>
        /// <param name="other">
        /// The Collider2D of the object the projectile has collided with.
        /// </param>
        /// <remarks>
        /// This method is called when the projectile collides with another object. If the object
        /// has the tag "Enemy", the projectile will apply damage to it and then be destroyed.
        /// </remarks>
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Enemy")) {
                Enemy.EnemyTakeDamage enemyTakeDamage = other.GetComponent<Enemy.EnemyTakeDamage>();
                if (enemyTakeDamage != null) {
                    enemyTakeDamage.TakeDamage(projectileDamage);
                }
                Destroy(gameObject);
            }
        }
    }
}

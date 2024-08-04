using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Projectile {
    public class FireProjectile : MonoBehaviour {

        [SerializeField]
        private Rigidbody2D rigidBody;
        [SerializeField]
        private float destroyDelayTime = 5f;  
        [SerializeField]
        private float projectileSpeed = 10f;
        [SerializeField]
        private float projectileDamage = 200f; 

        private float time;
        void Start() {

        }

        void Update() {
            time += Time.deltaTime;
            if(time >= destroyDelayTime) {
                Destroy(gameObject);
            }
        }

        public void LaunchProjectile(Vector2 targetPosition) {
            if (rigidBody != null) {
                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
                float projectileRotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, projectileRotation + 90);
                rigidBody.velocity = direction * projectileSpeed;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider) {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null) {
                // Hit a Damageable Object
                damageable.Damage(projectileDamage);
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Projectile {
    public class ProjectileBehavior : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float destroyTime = 5f;
        [SerializeField] private float projectileDamage = 100f;

        private void Start()
        {
            Invoke("DestroyProjectile", destroyTime);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy.EnemyTakeDamage>().Damage(projectileDamage);
                Debug.Log("Projectile hit " + collision.gameObject.name + " for " + projectileDamage + " damage.");
                DestroyProjectile();
            }
        }
        private void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }
}


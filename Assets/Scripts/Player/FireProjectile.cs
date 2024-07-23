using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField]
    float projectileSpeed = 50f;
    [SerializeField]
    Rigidbody2D rigidBody;

    private float time;
    private float delayTime = 5f;

    void Update() {
        time += Time.deltaTime;
        if(time >= delayTime) {
            Destroy(gameObject);
        }
    }

    public void LaunchProjectile(Vector2 targetPosition) {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float projectileRotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, projectileRotation + 90);
        rigidBody.velocity = direction * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }
}

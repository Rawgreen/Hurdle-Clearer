using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class Movement : MonoBehaviour
    {
        public Animator animator;

        private Vector2 currentPosition;  
        private Vector2 previousPosition;
        private GameObject player;
        private float moveSpeed;  
        private Vector2 movement;
        private Vector2 playerPosition; 


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Stats enemyStats = GetComponent<Stats>();
            if (enemyStats != null) {
                moveSpeed = enemyStats.GetMoveSpeed();
            }
            currentPosition = (Vector2)gameObject.transform.position;
            previousPosition = currentPosition;
        }

        void Update()
        {
            playerPosition = player.transform.position;

            // Move the current object towards the player
            currentPosition = Vector2.MoveTowards(currentPosition, playerPosition, moveSpeed * Time.deltaTime);
            transform.position = currentPosition;

            // Calculate movement vector
            movement = (currentPosition - previousPosition).normalized;

            // animator controls
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.magnitude);

            // Update previous position
            previousPosition = currentPosition;
        }
    }
}

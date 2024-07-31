using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    /// <summary>
    /// Manages the movement of the player character using physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        public Animator animator;

        [SerializeField]
        private Rigidbody2D rigidBody;
        private float movementX;  
        private float movementY;  
        private float moveSpeed;  

        void Start()
        {
            PlayerStats playerStats = GetComponent<PlayerStats>();
            if (playerStats != null){
                moveSpeed = playerStats.GetMoveSpeed();
            }
            if (rigidBody == null) {
                rigidBody = GetComponent<Rigidbody2D>();
            }
        }

        void Update()
        {
            movementX = Input.GetAxis("Horizontal");
            movementY = Input.GetAxis("Vertical");

            Vector2 input = new Vector2(movementX, movementY);
            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);
            animator.SetFloat("Speed", input.sqrMagnitude);

            /* Debug.Log("Input magnitude: " + input); */

            if(input.magnitude > 0) {
                rigidBody.velocity = input.normalized * moveSpeed;
            }
            else {
                rigidBody.velocity = Vector2.zero;
            }
        }

        public bool isPlayerOnMove() {
            if (rigidBody.velocity != Vector2.zero) {
                return true;
            } else {
                return false;
            }
        }
    }
}

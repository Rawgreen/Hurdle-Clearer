using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerMovement : MonoBehaviour
    {   
        private float movementX;  
        private float movementY;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float moveSpeed = 5f;
        [SerializeField]
        private Rigidbody2D rigidBody;

        void Start()
        {
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

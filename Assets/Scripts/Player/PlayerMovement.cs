using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerMovement : MonoBehaviour
    {   
        [SerializeField] private Animator animator;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private Rigidbody2D rigidBody;

        private float movementX;
        private float movementY;

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

            if(input.magnitude > 0) {
                rigidBody.velocity = input.normalized * moveSpeed;
            }
            else {
                rigidBody.velocity = Vector2.zero;
            }
        }

        public bool isPlayerOnMove() 
        {
            if (rigidBody.velocity != Vector2.zero) {
                return true;
            } else {
                return false;
            }
        }
    }
}

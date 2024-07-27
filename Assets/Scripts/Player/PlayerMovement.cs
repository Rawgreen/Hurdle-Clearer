using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    /// <summary>
    /// Manages the movement of the player character using physics.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rigidBody;  // The Rigidbody2D component used for movement.

        private float movementX;  // The horizontal input axis value.
        private float movementY;  // The vertical input axis value.
        
        private float moveSpeed;  // The speed at which the player moves.

        /// <summary>
        /// Initializes the player's movement settings.
        /// </summary>
        /// <remarks>
        /// This method retrieves the move speed from the PlayerStats component and ensures
        /// that the Rigidbody2D component is assigned.
        /// </remarks>
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

        /// <summary>
        /// Updates the player's movement based on user input.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame and sets the velocity of the Rigidbody2D
        /// based on the input from the horizontal and vertical axes. If there is input, the
        /// player's velocity is adjusted; otherwise, it is set to zero.
        /// </remarks>
        void Update()
        {
            movementX = Input.GetAxis("Horizontal");
            movementY = Input.GetAxis("Vertical");

            Vector2 input = new Vector2(movementX, movementY);

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

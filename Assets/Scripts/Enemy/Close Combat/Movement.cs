using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    /// <summary>
    /// Manages the movement of an enemy character towards the player.
    /// </summary>
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private Transform currentPosition;  // The transform of the enemy character.

        [SerializeField]
        private GameObject player;  // The player object to move towards.

        private float moveSpeed;  // The speed at which the enemy moves.

        private Vector2 playerPosition;  // The current position of the player.

        /// <summary>
        /// Initializes the enemy's movement settings.
        /// </summary>
        /// <remarks>
        /// This method finds the player object by its tag and retrieves the movement speed
        /// from the enemy's stats. It also sets the current position to the enemy's transform.
        /// </remarks>
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Stats enemyStats = GetComponent<Stats>();
            if (enemyStats != null) {
                moveSpeed = enemyStats.GetMoveSpeed();
            }
            currentPosition = this.transform;
        }

        /// <summary>
        /// Updates the enemy's position to move towards the player.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame and moves the enemy towards the player based on
        /// the calculated speed and direction.
        /// </remarks>
        void Update()
        {
            playerPosition = player.transform.position;

            // Move the current object towards the player
            currentPosition.position = Vector2.MoveTowards(currentPosition.position, playerPosition, moveSpeed * Time.deltaTime);
        }
    }
}

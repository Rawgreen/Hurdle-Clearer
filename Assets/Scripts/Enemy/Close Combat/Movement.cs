using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private Transform currentPosition;  
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private Animator animator;
  
        private float moveSpeed;  
        private Vector2 playerPosition; 


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Stats enemyStats = GetComponent<Stats>();
            if (enemyStats != null) {
                moveSpeed = enemyStats.GetMoveSpeed();
            }
            currentPosition = gameObject.transform;
        }

        void Update()
        {
            playerPosition = player.transform.position;
            // Move the current object towards the player
            currentPosition.position = Vector2.MoveTowards(currentPosition.position, playerPosition, moveSpeed * Time.deltaTime);
            animator.SetInteger("AnimState", 2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidBody;

    private float movementX;
    private float movementY;
    
    float moveSpeed;

    // Start is called before the first frame update
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

    // Update is called once per frame
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
}

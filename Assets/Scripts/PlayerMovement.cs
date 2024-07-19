using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidBody;

    [SerializeField]
    float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if (rigidBody == null) {
            rigidBody = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(xInput, yInput);

        Debug.Log("Input magnitude: " + input);

        if(input.magnitude > 0) {
            rigidBody.velocity = input.normalized * speed;
        }
        else {
            rigidBody.velocity = Vector2.zero;
        }
    }
}

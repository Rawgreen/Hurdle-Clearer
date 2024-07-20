using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_Enemy_Movement : MonoBehaviour
{

    [SerializeField]
    Transform objectPosition;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float speed = 2.5f;

    private Vector2 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        objectPosition = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;

        // Move the current object towards the player
        objectPosition.position = Vector2.MoveTowards(objectPosition.position, playerPosition, speed * Time.deltaTime);
    }
}

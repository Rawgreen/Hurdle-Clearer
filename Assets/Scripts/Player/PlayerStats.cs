using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    float playerHealth = 100f;
    [SerializeField]
    float attackDamage = 10f;

    [SerializeField]
    float moveSpeed = 5f;

    public float GetMoveSpeed() {
        return moveSpeed;
    }
}   

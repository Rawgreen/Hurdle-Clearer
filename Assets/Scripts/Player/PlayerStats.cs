using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    float playerHealth = 100f;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float attackDamage = 10f;

    [SerializeField]
    float attackDelay = 1f;

    [SerializeField]
    float attackDistance = 5f;

    public float GetPlayerHealth() {
        return playerHealth;
    }

    public float GetAttackDamage() {
        return attackDamage;
    }

    public float GetMoveSpeed() {
        return moveSpeed;
    }

    public float GetAttackDelay() {
        return attackDelay;
    }

    public float GetAttackDistance() {
        return attackDistance;
    }
 }   

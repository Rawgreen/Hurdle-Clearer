using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCEnemyStats : MonoBehaviour
{

    [SerializeField]
    float enemyHealth = 30f;

    [SerializeField]
    float attackDamage = 10f;

    [SerializeField]
    float moveSpeed = 1.5f;

    public float GetEnemyHealth() {
        return enemyHealth;
    }

    public float GetAttackDamage() {
        return attackDamage;
    }

    public float GetMoveSpeed() {
        return moveSpeed;
    }

}

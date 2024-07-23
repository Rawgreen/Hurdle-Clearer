using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    GameObject projectileLaunchPosition;
    [SerializeField]
    bool drawAttackDistance = true;
    [SerializeField]
    float initialAttackDistance = 4f;

    PlayerStats playerStats;

    private float attackDistance;
    private float attackDelay;
    private GameObject enemyObject;
    private float enemyDistance;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            attackDistance = playerStats.GetAttackDistance();
            attackDelay = playerStats.GetAttackDelay();
        }
        /* else
        {
            attackDistance = initialAttackDistance;
        } */
    }

    void Update() {
        enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        if (enemyObject != null)
        {
            enemyDistance = (enemyObject.transform.position - transform.position).magnitude;
        }

        if (enemyDistance <= attackDistance)
        {
            delay += Time.deltaTime;
            if (delay >= attackDelay)
            {
                FireProjectile();
                delay = 0;
            }
        }
    }
    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileLaunchPosition.transform.position, Quaternion.identity);
        FireProjectile fireProjectile = projectile.GetComponent<FireProjectile>();
        if(fireProjectile != null) {
            fireProjectile.LaunchProjectile(enemyObject.transform.position);
        }
    }

    /* void OnDrawGizmos()
    {
        if (drawAttackDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, initialAttackDistance);
        }
    } */
}

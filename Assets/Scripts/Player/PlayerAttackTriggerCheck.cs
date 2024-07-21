using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTriggerCheck : MonoBehaviour
{
    [SerializeField]
    Collider2D attackCollider;

    private void OnTriggerEnter2D(Collider2D other) {
        // logic here
        GameObject[] enemyStats = GameObject.FindGameObjectsWithTag("Close Combat Enemy");

        foreach(GameObject enemy in enemyStats) {
            CCEnemyStats currentEnemyStats = enemy.GetComponent<CCEnemyStats>();

            if(currentEnemyStats != null) {
                // Attack logic here
            }
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
        // logic here
    }

    private void OnTriggerExit2D(Collider2D other) {
        // logic here
    }
}

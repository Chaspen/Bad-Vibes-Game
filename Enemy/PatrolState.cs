using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyAI
{
    EnemyStates enemy;
    int nextWaypoint = 0;

    public PatrolState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Watch();
        Patrol();
    }

    void Watch()
    {
        RaycastHit hit;

        if (Physics.Raycast(enemy.transform.position, -enemy.transform.forward, out hit, enemy.patrolRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                enemy.chaseTarget = hit.transform;
                ToChaseState();
            }
        }
    }

    void Patrol()
    {
        enemy.navMeshAgent.destination = enemy.waypoints[nextWaypoint].position;
        enemy.navMeshAgent.Resume();

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWaypoint = (nextWaypoint + 1) % enemy.waypoints.Count;
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToPatrolState()
    {
        //Debug.Log("I am patrolling already!");
    }

    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    enum AIState {
        Idle, Patrolling, Chasing
    }

    [SerializeField] private Transform Waypoints;
    [SerializeField] private float WaitAtPoint = 2f ;

    private int CurrentWaypoint;
    private float WaitCounter;

    NavMeshAgent agent;

    [SerializeField] private AIState CurrentState;

    [SerializeField] private float ChaseRange;

    private GameObject player;
   
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        WaitCounter = WaitAtPoint;
    }


    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (CurrentState)
        {
            case AIState.Idle:

                if (WaitCounter > 0)
                {
                    WaitCounter -= Time.deltaTime;
                }
                else
                {
                    CurrentState = AIState.Patrolling;
                    agent.SetDestination(Waypoints.GetChild(CurrentWaypoint).position);
                }

                if (distanceToPlayer <= ChaseRange)
                {
                    CurrentState = AIState.Chasing;
                }

                break;

                case AIState.Patrolling:

                if (agent.remainingDistance <= 0.2f)
                {
                    CurrentWaypoint++;
                    if (CurrentWaypoint >= Waypoints.childCount)
                    {
                        CurrentWaypoint = 0;
                    }
                    CurrentState = AIState.Idle;
                    WaitCounter = WaitAtPoint;
                }

                if (distanceToPlayer <= ChaseRange)
                {
                    CurrentState = AIState.Chasing;
                }


                break;

                case AIState.Chasing:

                agent.SetDestination(player.transform.position);
                if (distanceToPlayer > ChaseRange)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                   
                }

                break;

        }

    }
}

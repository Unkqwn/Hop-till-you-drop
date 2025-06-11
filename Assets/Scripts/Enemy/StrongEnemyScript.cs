using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StrongEnemyScript : MonoBehaviour
{


    enum AIState
    {
        Idle, Patrolling, Chasing, Attacking
    }

    [SerializeField] private Transform[] Waypoints;
    [SerializeField] private float WaitAtPoint = 2f;

    private int CurrentWaypoint;
    private float WaitCounter;
    private float attackCounter;
    private bool alreadyAttacked;
    private GameObject player;

    public float timeBetweenAttacks;
    public GameObject bullet;
    public GameObject left;
    public GameObject middle;
    public GameObject right;

    NavMeshAgent agent;

    [SerializeField] private AIState CurrentState;

    [SerializeField] private float ChaseRange;

    [SerializeField] private float AttackRange;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        WaitCounter = WaitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = 20;
        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        }

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

                }

                if (distanceToPlayer <= ChaseRange)
                {
                    CurrentState = AIState.Chasing;
                }

                break;

            case AIState.Patrolling:



                if (distanceToPlayer <= ChaseRange)
                {
                    CurrentState = AIState.Chasing;
                }


                break;

            case AIState.Chasing:

                if (distanceToPlayer <= AttackRange)
                {
                    CurrentState = AIState.Attacking;
                }


                if (distanceToPlayer > ChaseRange)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;

                }
                else
                {

                    if (player != null)
                    {
                        agent.SetDestination(player.transform.position);
                    }

                    agent.isStopped = false;
                }

                break;

            case AIState.Attacking:

                if (player != null)
                {
                    agent.SetDestination(player.transform.position);
                    AttackPlayer();
                }

                break;

        }
     }

    private void AttackPlayer()
    {
        if (!alreadyAttacked) { 
       
        //left
        Rigidbody rb = Instantiate(bullet, left.transform.position, left.transform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * 10f, ForceMode.Impulse);
        Destroy(rb.gameObject, 5f);

        Rigidbody rb2 = Instantiate(bullet, middle.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb2.AddForce(transform.forward * 10f, ForceMode.Impulse);
        Destroy(rb2.gameObject, 5f);

        Rigidbody rb3 = Instantiate(bullet, right.transform.position, right.transform.rotation).GetComponent<Rigidbody>();
        rb3.AddForce(rb3.transform.forward * 10f, ForceMode.Impulse);
        Destroy(rb3.gameObject, 5f);



            alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


}

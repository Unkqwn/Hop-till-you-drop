using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int numberOfProjectiles;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public GameObject actualSpawnPoint;


    [Header("Private Variables")]
    private Vector3 startPoint;
    private const float radius = 5f;
    private bool higherBulletCount;

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
    private Vector3 LookAtFix;

    public float timeBetweenAttacks;
    public GameObject bullet;
    public GameObject shootingDirLeft;
    public GameObject shootingDirRight;
    [SerializeField] private GameObject forwardDir;

    NavMeshAgent agent;

    [SerializeField] private AIState CurrentState;

    [SerializeField] private float ChaseRange;

    [SerializeField] private float AttackRange;

   

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        WaitCounter = WaitAtPoint;
        higherBulletCount = false;
    }


    void Update()
    {
        startPoint = transform.position;

        float distanceToPlayer = 20;
        if (player != null )
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

                if (distanceToPlayer > AttackRange)
                {
                    CurrentState = AIState.Chasing;

                }

                break;

        }

    }

    private void AttackPlayer()
    {

        //transform.forward = new Vector3(forwardDir.transform.forward.x, transform.forward.y, forwardDir.transform.forward.z);
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
        
        Vector3 lookat;
        lookat = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        transform.LookAt(player.transform.position + lookat);

        agent.isStopped = true;

        if (!alreadyAttacked)
        {

            if (higherBulletCount == true)
            {
               
                numberOfProjectiles = 10;
                higherBulletCount = false;
            }
            else
            {
                
                numberOfProjectiles = 8;
                higherBulletCount = true;
            }

           
            Debug.Log("numberofprojectiles: " + numberOfProjectiles);
            Vector3 shootingDir1 = new Vector3(0.5f, 0, 0).normalized;
            Vector3 shootingDir2 = new Vector3(-0.5f, 0, 0).normalized;

            //bullet 1
            Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);

           
           
            Destroy(rb.gameObject, 5f);



            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

   

   



}

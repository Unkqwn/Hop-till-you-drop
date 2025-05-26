using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
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

    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject bullet;

    NavMeshAgent agent;

    [SerializeField] private AIState CurrentState;

    [SerializeField] private float ChaseRange;

    [SerializeField] private float AttackRange;

    private GameObject player;

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
                    agent.SetDestination(player.transform.position);
                    agent.isStopped = false;
                }

                break;

            case AIState.Attacking:
                agent.SetDestination(player.transform.position);
                AttackPlayer();
                break;

        }

    }

    private void AttackPlayer()
    {
        transform.LookAt(player.transform.position);




        if (!alreadyAttacked)
        {

            //SpawnProjectiles(numberOfProjectiles);

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
            Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.Euler(0, 10, 0)).GetComponent<Rigidbody>();

            ////rb.transform.Rotate(Vector3.up, 10);
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            Destroy(rb.gameObject, 5f);

            //Rigidbody rb2 = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb2.AddForce(transform.forward * 10f, ForceMode.Impulse);

            //Rigidbody rb3 = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb3.AddForce((transform.forward + shootingDir2) * 10f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void SpawnProjectiles(int _numberOfProjectiles)
    {
        float angleStep = 360f / _numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < _numberOfProjectiles; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.z + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, 0, projectileDirYPosition);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(projectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.z);
            tmpObj.gameObject.layer = LayerMask.NameToLayer("E_bullet");
            Destroy(tmpObj, 5f);
            angle += angleStep;



        }

        
    }

    private void DoSpawnProjectiles()
    {
        SpawnProjectiles(numberOfProjectiles);
    }



}

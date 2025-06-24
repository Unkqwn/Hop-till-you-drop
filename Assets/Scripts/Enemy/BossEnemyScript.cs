using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyScript : MonoBehaviour
{
    private bool alreadyAttacked;
    private GameObject player;
    private float WaitCounter;
    private bool changeRow;
    public float timeBetweenAttacks;
    private bool isAttacking;
    
    //directions shootingrow 1
    public GameObject left;
    public GameObject secondLeft;
    public GameObject right;
    public GameObject secondRight;
    public GameObject middle;

    //directions shootingrow2
    public GameObject middle1;
    public GameObject middle2;
    public GameObject sr2_right1;
    public GameObject sr2_right2;
    public GameObject sr2_left1;
    public GameObject sr2_left2;
   

        
    //bullet types
    public GameObject bullet;
    public GameObject homingBullet;
    public GameObject splittingBullet;
    public GameObject heartShot;
    


    enum AIState
    {
        Idle, Patrolling, Chasing, Attacking
    }

    NavMeshAgent agent;

    [SerializeField] private AIState CurrentState;

    [SerializeField] private float ChaseRange;

    [SerializeField] private float AttackRange;

    [SerializeField] private float WaitAtPoint = 2f;




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

        if (isAttacking == true)
        {
            agent.speed = 0;
        } 
        else
        {
            agent.speed = 2;
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
                isAttacking = false;

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
                    transform.LookAt(player.transform.position);
                    agent.SetDestination(player.transform.position);
                   
                    AttackPlayer();
                }

                isAttacking = true;

                if (distanceToPlayer > AttackRange)
                {
                    CurrentState = AIState.Chasing;
                  
                }

                break;

        }
    }

    private void AttackPlayer()
    {
        if (!alreadyAttacked)
        {

            if (changeRow == true)
            {
                Rigidbody rb6 = Instantiate(bullet, middle1.transform.position, middle1.transform.rotation).GetComponent<Rigidbody>();
                rb6.AddForce(rb6.transform.forward * 5f, ForceMode.Impulse);

                Rigidbody rb7 = Instantiate(bullet, middle2.transform.position, middle2.transform.rotation).GetComponent<Rigidbody>();
                rb7.AddForce(rb7.transform.forward * 5f, ForceMode.Impulse);

                Rigidbody rb8 = Instantiate(bullet, sr2_right1.transform.position, sr2_right1.transform.rotation).GetComponent<Rigidbody>();
                rb8.AddForce(rb8.transform.forward * 5f, ForceMode.Impulse);

                Rigidbody rb9 = Instantiate(bullet, sr2_left1.transform.position, sr2_left1.transform.rotation).GetComponent<Rigidbody>();
                rb9.AddForce(rb9.transform.forward * 5f, ForceMode.Impulse);

                Rigidbody rb10 = Instantiate(bullet, sr2_right2.transform.position, sr2_right2.transform.rotation).GetComponent<Rigidbody>();
                rb10.AddForce(rb10.transform.forward * 5f, ForceMode.Impulse);

                Rigidbody rb11 = Instantiate(bullet, sr2_left2.transform.position, sr2_left2.transform.rotation).GetComponent<Rigidbody>();
                rb11.AddForce(rb11.transform.forward * 5f, ForceMode.Impulse);

                changeRow = false;
            }
            else
            {

                Rigidbody rb = Instantiate(bullet, left.transform.position, left.transform.rotation).GetComponent<Rigidbody>();
                rb.AddForce(rb.transform.forward * 5f, ForceMode.Impulse);


                Rigidbody rb2 = Instantiate(bullet, middle.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb2.AddForce(transform.forward * 5f, ForceMode.Impulse);


                Rigidbody rb3 = Instantiate(bullet, right.transform.position, right.transform.rotation).GetComponent<Rigidbody>();
                rb3.AddForce(rb3.transform.forward * 5f, ForceMode.Impulse);


                Rigidbody rb4 = Instantiate(bullet, secondLeft.transform.position, secondLeft.transform.rotation).GetComponent<Rigidbody>();
                rb4.AddForce(rb4.transform.forward * 5f, ForceMode.Impulse);

                Rigidbody rb5 = Instantiate(bullet, secondRight.transform.position, secondRight.transform.rotation).GetComponent<Rigidbody>();
                rb5.AddForce(rb5.transform.forward * 5f, ForceMode.Impulse);

                changeRow = true;
            }
           
            







            //Destroy(rb3.gameObject, 10f);






            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using Random = System.Random;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask isThisGround, isThisPlayer;

    public int enemyHP = 100;

    public Slider healthBar;

    [SerializeField] public Animator _animator;

    public bool gettingDamaged = false;
   
    //Enemy patroling variables

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    //Attacking variables

    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    
    //States

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    // Other scripts

    public HealthBar hb;

    private void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isThisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isThisPlayer);
        
        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
            AttackPlayer();

        healthBar.value = enemyHP;

        agent.speed = 10;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void EnemyTakeDamage(int damageAmount, Animator _animator)
    {
        _animator.SetBool("takeDamage", true);
        enemyHP -= damageAmount;
        gettingDamaged = true;
        if (enemyHP <= 0)
        {
            enemyHP = 0;
            Debug.Log("enemy die");
        }
        else
        {
            Debug.Log("enemy is taking damage");
        }
    }
    private void Patroling()
    {
        _animator.SetBool("takeDamage", false);
        _animator.SetBool("Attack", false);
        Debug.Log("Patroling");
        if (!walkPointSet)
            SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //Player has been reached

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        // Check if the point is on the map and not outside
        if (Physics.Raycast(walkPoint, -transform.up, 5f, isThisGround))
            walkPointSet = true;

    }

    private void ChasePlayer()
    {
        Debug.Log("Chasing!!");
        agent.SetDestination(player.position);
        _animator.SetBool("Attack", false);
        _animator.SetBool("takeDamage", false);
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking");
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        _animator.SetBool("takeDamage", false);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Debug.Log("Attacking");
            // Add my attack here
            _animator.SetBool("Attack", true);
            hb.TakeDamage(10);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    /*
    private void OnAnimatorMove()
    {
        if (_animator.GetBool("Attack") == false)
        {
            agent.speed = (_animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }*/

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

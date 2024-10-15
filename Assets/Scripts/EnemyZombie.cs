using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyZombie : MonoBehaviour
{
    public Transform player;
    NavMeshAgent navAgent;
    public float enemyDetectionRange;
    public float enemyAttackRange;
    public Animator animator;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        FollowPlayer();
        AttackPlayer();
    }

    public void FollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 currentVelocity = navAgent.velocity;

        if (distanceToPlayer <= enemyDetectionRange)
        {
            navAgent.SetDestination(player.position);
            animator.SetInteger("Zombie Animation State", 1);

        }
        else if (currentVelocity.magnitude <= 0)
        {
            animator.SetInteger("Zombie Animation State", 0);

        }

    }

    public void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 currentVelocity = navAgent.velocity;

        if (distanceToPlayer <= enemyAttackRange)
        {
            navAgent.SetDestination(player.position);
            animator.SetInteger("Zombie Animation State", 2);
        }
        else if (currentVelocity.magnitude <= 0)
        {
            animator.SetInteger("Zombie Animation State", 0);

        }


    }


}
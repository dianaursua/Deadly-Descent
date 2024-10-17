using UnityEngine;
using UnityEngine.AI;

public class EnemyZombie : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navAgent;
    public float enemyDetectionRange;
    public float enemyAttackRange;
    public Animator animator;

    public Transform[] waypoints; // Array of waypoints
    private int currentWaypointIndex = 0; // Index of the current waypoint
    public float waypointReachThreshold = 1f; // Distance to consider as reached

    public int zombieDamage = 10; // Damage zombie deals to the player
    public float attackCooldown = 2f; // Time between attacks
    private float nextAttackTime = 0f; // Timer for next attack

    private PlayerHealth playerHealth; // Reference to player's health script

    private Rigidbody rb; // Rigidbody component reference

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Ensure the player has the PlayerHealth component
        playerHealth = player.GetComponent<PlayerHealth>();

        // Set Rigidbody properties
        rb.isKinematic = false; // Not kinematic to allow physics interaction
        rb.useGravity = true;   // Enable gravity for natural falling behavior
        rb.freezeRotation = true; // Freeze rotation to prevent flipping over

        GoToNextWaypoint(); // Start patrolling
    }

    private void Update()
    {
        // Call the methods to follow and attack the player
        FollowPlayer();
        AttackPlayer();
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= enemyDetectionRange)
        {
            // Set the destination to the player's position
            navAgent.SetDestination(player.position);
        }
        else
        {
            // If out of range, patrol
            Patrol();
        }
    }

    private void Patrol()
    {
        // Check if the zombie is close to the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < waypointReachThreshold)
        {
            // Go to the next waypoint
            GoToNextWaypoint();
        }
        else
        {
            // Continue moving towards the current waypoint
            navAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    private void GoToNextWaypoint()
    {
        // Move to the next waypoint in the array
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop back to the first waypoint
    }

    private void AttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within attack range and if the zombie can attack again
        if (distanceToPlayer <= enemyAttackRange && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown; // Set next attack time

            // Stop the zombie from moving when it is attacking the player
            navAgent.isStopped = true;

            // Check if playerHealth reference is valid
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(zombieDamage); // Apply damage to player
            }
        }
        else if (distanceToPlayer > enemyAttackRange)
        {
            // Resume movement if player moves out of attack range
            navAgent.isStopped = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.transform == player)
        {
            // Optionally, stop the zombie from moving if colliding with the player
            // This can be left out if you want the player to push the zombie
            // navAgent.isStopped = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Resume movement when no longer colliding with the player
        if (collision.transform == player)
        {
            // You can uncomment the following line if you want to stop movement
            // navAgent.isStopped = false;
        }
    }
}

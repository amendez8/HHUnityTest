using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public LayerMask isGround;
    public LayerMask isPlayer;
    public int health;
    Rigidbody rb;

    // Roaming
    private Vector3 distanceToPoint;
    public Vector3 walking;
    bool walkingSet;
    public float walkingRange;

    // Attacking
    public float timeBetweenAttacks;
    public float attackSpeed;
    public float attackElevation;
    bool attacking;
    public GameObject projectile;

    // States - check if player is in sight/range to be attacked.
    public float sightDistance;
    public float attackDistance;
    public bool inSightRange;
    public bool inAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check to see if player is in chase and/or attack range
        InRange();

        // if not in either range
        if (!inSightRange && !inAttackRange)
            Roaming();
        // if just in chase range
        if (inSightRange && !inAttackRange)
            Chase();
        // if in both chase and attack range
        if (inSightRange && inAttackRange)
            Attack();

    }

    private void InRange()
    {
        // Checking distance of player to enemy to determine if they're in view to start following and in range to start attacking.
        inSightRange = Physics.CheckSphere(transform.position, sightDistance, isPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackDistance, isPlayer);
    }

    private void Roaming()
    {
        // Start walk around
        if (!walkingSet)
            WalkAround(); //SearchWalkPoint

        if (walkingSet)
            navMeshAgent.SetDestination(walking);

        // point to walk to is reached
        if (distanceToPoint.magnitude < 1f)
            walkingSet = false;

    }

    private void Chase()
    {
        // Go to player's location
        navMeshAgent.SetDestination(player.position);
    }

    private void Attack()
    {
        // Stop enemy from moving when attacking
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(player);

        // if Enemy is not attacking, attack
        if (!attacking)
        {
            // Create Enemy projectile and shoot it
            rb = Instantiate(projectile, (new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z - 0.5f)), Quaternion.identity).GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(transform.forward * attackSpeed, ForceMode.Impulse);
            rb.AddForce(transform.up * attackElevation, ForceMode.Impulse);

            attacking = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void WalkAround()
    {
        // randomZ Find next spot for enemy to walk to
        float randomPointZ = Random.Range(-walkingRange, walkingRange);
        float randomPointX = Random.Range(-walkingRange, walkingRange);

        walking = new Vector3(transform.position.x + randomPointX, transform.position.y, transform.position.z + randomPointZ);

        // checking if new point is on map
        if (Physics.Raycast(walking, -transform.up, 2f, isGround))
        {
            walkingSet = true;
        }
    }

    private void ResetAttack()
    {
        attacking = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        Rock playerRock = col.gameObject.GetComponent<Rock>();
        Arrow playerArrow = col.gameObject.GetComponent<Arrow>();

        if (playerRock && !col.transform.root.CompareTag("Loot"))
            health -= playerRock.damage;
        
        if (playerArrow && !col.transform.root.CompareTag("Loot"))
            health -= playerArrow.damage;

        if (health <= 0)
            KillEnemy();

    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightDistance);
    }



}

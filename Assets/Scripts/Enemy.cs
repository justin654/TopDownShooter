using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float stoppingDistance = 3f;
    public float retreatDistance = 1.5f;
    public float wanderTime = 2f; // The time between wander movements
    public float wanderRadius = 5f; // The radius within which the enemy will choose a new wander point

    private Transform player;
    private Vector2 targetPosition; // The current target point the enemy is wandering towards
    private bool isWandering = false;
    private float timeSinceLastWanderPoint = 0f; // Timer to keep track of wandering

    // Enum to define the enemy's states
    private enum EnemyState
    {
        wandering,
        chasing,
        retreating
    }

    private EnemyState currentState;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Please ensure your player GameObject has the tag 'Player'.");
        }

        currentState = EnemyState.wandering; // Start with the wandering state
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Check the distance and update the state accordingly
            if (distanceToPlayer > stoppingDistance)
            {
                currentState = EnemyState.chasing;
            }
            else if (distanceToPlayer < retreatDistance)
            {
                currentState = EnemyState.retreating;
            }
            else
            {
                currentState = EnemyState.wandering;
            }

            // Action based on state
            switch (currentState)
            {
                case EnemyState.chasing:
                    ChasePlayer();
                    break;
                case EnemyState.retreating:
                    RetreatFromPlayer();
                    break;
                case EnemyState.wandering:
                    if (!isWandering && timeSinceLastWanderPoint >= wanderTime)
                    {
                        StartCoroutine(Wander());
                    }
                    break;
            }
        }

        timeSinceLastWanderPoint += Time.deltaTime; // Update the wandering timer
    }

    void ChasePlayer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);
    }

    void RetreatFromPlayer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, -step); // Move away from the player
    }

    private IEnumerator Wander()
    {
        isWandering = true;

        while (currentState == EnemyState.wandering)
        {
            // Create a random target position
            Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
            targetPosition = (Vector2)transform.position + randomPoint;

            // Reset the wander timer
            timeSinceLastWanderPoint = 0f;

            // Log the target position
            Debug.Log($"Wandering: Moving to position {targetPosition}");

            // Move towards the target position
            while (Vector2.Distance(transform.position, targetPosition) > 0.1f) // Keep moving until close enough
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
                yield return null; // Wait for a frame before re-evaluating the loop condition
            }

            // After reaching the target, wait for the defined period before wandering again
            yield return new WaitForSeconds(wanderTime);
        }

        isWandering = false; // Wander routine ended
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntBehavior : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float detectionRange = 5f; // Range within which the ant detects the player
    public float speed = 1.5f; // Speed of the ant
    public int health = 1; // Health of the ant
    public GameObject foodPrefab;

    private NavMeshAgent agent;
    private bool isFollowing = false;
    private Vector3 initialPosition; // Store the initial position

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        initialPosition = transform.position; // Store the initial position at start
    }

    void Update()
    {
        // Check the distance between the ant and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        // If the player is within detection range, start following
        if (distanceToPlayer <= detectionRange)
        {
            isFollowing = true;
            transform.LookAt(player.position);
        }
        else
        {
            isFollowing = false;
        }

        // If following, set the player's position as the destination
        if (isFollowing)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Debug.Log(Vector3.Distance(initialPosition, transform.position) + " is the distance");
            if (Vector3.Distance(initialPosition, transform.position) < 0.5f)
            {
                agent.ResetPath();
                return;
            }
            // If not following, return to the initial position
            agent.SetDestination(initialPosition);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reduce health of the ant
            health -= 1;

            // Check if the ant is dead
            if (health <= 0)
            {
                Instantiate(foodPrefab, transform.position + (Vector3.up*1f), foodPrefab.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}

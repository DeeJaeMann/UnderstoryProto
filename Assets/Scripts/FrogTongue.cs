using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongue : MonoBehaviour
{
    private Vector3 initialPosition; // Starting position of the tongue (frog's mouth)
    private Vector3 targetPosition; // Position to move towards (player's position)
    private float speed; // Speed of the tongue
    private bool isDraggingPlayer = false; // Flag to check if the tongue is dragging the player
    private Transform player; // Reference to the player's transform
    private Rigidbody tongueRigidbody; // Rigidbody of the tongue

    public void Initialize(Vector3 targetPos, float tongueSpeed)
    {
        initialPosition = transform.position;
        targetPosition = targetPos;
        speed = tongueSpeed;
        tongueRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move the tongue towards the target position
        if (!isDraggingPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the tongue has reached the player
            if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
            {
                isDraggingPlayer = true;
                player = GameObject.FindGameObjectWithTag("Player").transform;
                tongueRigidbody.isKinematic = true; // Stop the tongue's movement
            }
        }
        else
        {
            // Drag the player back to the frog's mouth
            player.position = Vector3.MoveTowards(player.position, initialPosition, speed * Time.deltaTime);

            // Move the tongue back to the frog's mouth
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);

            // If the player reaches the frog, trigger death
            if (Vector3.Distance(player.position, initialPosition) == 0)
            {
                // Handle player death (e.g., restart the game, show game over screen)
                Debug.Log("Player is dead!");
                // Add more logic here for handling player death
                //Destroy(player.gameObject); // Destroy the player for now
                Destroy(gameObject); // Destroy the tongue
            }
        }
    }
}

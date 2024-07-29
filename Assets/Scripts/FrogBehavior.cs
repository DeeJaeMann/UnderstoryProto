using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBehavior : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float detectionRange = 10f; // Range within which the frog detects the player
    public Transform tongueSpawnPoint; // Point where the tongue attack originates
    public GameObject tonguePrefab; // Prefab of the frog's tongue
    public float tongueSpeed = 20f; // Speed of the tongue attack
    public float attackInterval = 2f; // Interval between attacks
    public Transform parent;

    private Vector3 playerLastPosition;
    private float lastAttackTime;

    void Update()
    {
        // Check the distance between the frog and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within detection range, look at the player and initiate the tongue attack
        if (distanceToPlayer <= detectionRange)
        {
            LookAtPlayer();

            if (Time.time > lastAttackTime + attackInterval)
            {
                playerLastPosition = player.position;
                TongueAttack();
                lastAttackTime = Time.time;
            }
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void TongueAttack()
    {
        GameObject tongue = Instantiate(tonguePrefab, tongueSpawnPoint.position, tonguePrefab.transform.rotation, parent);
        FrogTongue tongueScript = tongue.GetComponent<FrogTongue>();
        tongueScript.Initialize(playerLastPosition, tongueSpeed);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position to represent detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

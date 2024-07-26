using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderBehavior : MonoBehaviour
{
    public enum enemyStates { Return, Follow, Attack };
    public enemyStates currentState;
    public float detectionRadius = 10f;
    public float speed = 0.5f;
    private Transform _player;
    private Transform _spawnPoint;
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _spawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        currentState = enemyStates.Return;
        Debug.Log($"Spawn point is {_spawnPoint.position}");
        MoveToSpawnPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == enemyStates.Return)
        {
            MoveToSpawnPoint();
        }
        else if (currentState == enemyStates.Follow)
        {
            MoveToPlayer();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Spider detected player");
            currentState = enemyStates.Follow;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Spider lost player");
            currentState = enemyStates.Return;
        }
    }

    void MoveToSpawnPoint()
    {
        _agent.destination = _spawnPoint.position;
    }

    void MoveToPlayer()
    {
        _agent.destination = _player.position;
    }
}

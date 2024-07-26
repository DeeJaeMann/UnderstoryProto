using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderBehavior : MonoBehaviour
{
    public enum EnemyStates { Return, Follow, Attack, OffWeb };
    public EnemyStates currentState;
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
        currentState = EnemyStates.Return;
        Debug.Log($"Spawn point is {_spawnPoint.position}");
        MoveToSpawnPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == EnemyStates.Return)
        {
            MoveToSpawnPoint();
        }
        else if (currentState == EnemyStates.Follow)
        {
            MoveToPlayer();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" && currentState != EnemyStates.OffWeb)
        {
            Debug.Log("Spider detected player");
            currentState = EnemyStates.Follow;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Spider lost player");
            currentState = EnemyStates.Return;
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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class SpiderBehavior : MonoBehaviour
{
    public enum EnemyStates { Return, Follow, Attack };
    public EnemyStates currentState;
    public float detectionRadius = 10f;
    public float speed = 0.5f;
    public float attackRange = 0.8f;
    private Transform _player;
    private Transform _spawnPoint;
    private NavMeshAgent _agent;
    public float playerPushback = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _spawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        currentState = EnemyStates.Return;
        Debug.Log($"Spawn point is {_spawnPoint.position}");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(_player.position, transform.position) < attackRange)
        {
            _agent.ResetPath();
            AttackState();
        }
        if (currentState == EnemyStates.Return)
        {
            MoveToSpawnPoint();
        }
        else if (currentState == EnemyStates.Follow)
        {
            MoveToPlayer();
        }
        else if(currentState == EnemyStates.Attack)
        {
            Attack();
        }
    }

    void MoveToSpawnPoint()
    {
        _agent.destination = _spawnPoint.position;
    }

    void MoveToPlayer()
    {
        _agent.destination = _player.position;
        transform.LookAt(_player.position);
    }

    public void EnterFollowState()
    {
        if (Vector3.Distance(_player.position, transform.position) > attackRange)
        {
            currentState = EnemyStates.Follow;
            Debug.Log("Following Player");
        }
    }

    public void ReturnToSpawn()
    {
        if (Vector3.Distance(_player.position, transform.position) > attackRange)
        {
            currentState = EnemyStates.Return;
            Debug.Log("Returning to SpawnPoint");
        }
    }
    public void AttackState()
    {
        currentState = EnemyStates.Attack;
        Debug.Log("Attacking Player");
    }

    void Attack()
    {
        currentState = EnemyStates.Follow;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = (_player.position - transform.position).normalized;
            Rigidbody spiderRb = GetComponent<Rigidbody>();
            if(spiderRb != null ) 
            {
                spiderRb.AddForce(-direction * playerPushback, ForceMode.Impulse);
            }
        }
    }
}

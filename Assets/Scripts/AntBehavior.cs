using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private Vector3 startPos;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, startPos) > 0.1f)
        {
            return;
        }
        transform.position = startPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            agent.destination = other.transform.position;
            Debug.Log("Ant detected player");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            agent.destination = startPos;
            Debug.Log("Ant lost player");
        }
    }
}

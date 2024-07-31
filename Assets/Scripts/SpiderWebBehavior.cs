using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class SpiderWebBehavior : MonoBehaviour
{
    public float slowModifier = .5f;
    private PlayerBehavior player;
    private GameBehavior gameManager;
    private SpiderBehavior spiderBehavior;
    private float playerBaseSpeed;
    private string previousMessage;
    private Rigidbody _rigidBody;
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
        previousMessage = gameManager.progressText.text;
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        spiderBehavior = GameObject.Find("Spider").GetComponent<SpiderBehavior>();
        playerBaseSpeed = player.speed;
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updatePosition = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(spiderBehavior != null)
            {
                spiderBehavior.EnterFollowState();
            }
            if (player.speed == playerBaseSpeed)
            {
                player.speed *= slowModifier;
                gameManager.progressText.text = "Why is this ground so sticky?";
                gameManager.progressText.fontSize = 30;
            }
            //Debug.Log($"Player Speed is now {player.speed}");
            // Spawn Spider and set status to follow player
        }
    }

    private void OnCollisionExit(Collision collision)
    { 
        if (collision.gameObject.tag == "Player")
        {
            if (spiderBehavior != null)
            {
                spiderBehavior.ReturnToSpawn();
            }
            player.speed = playerBaseSpeed;
            gameManager.progressText.text = previousMessage;
            gameManager.progressText.fontSize = 36;
            //Debug.Log($"Player speed reset to {player.speed}");
            // Set Spider status to return
        }
        //if (collision.gameObject.tag == "Spider")
        //{
        //    Debug.Log("Spider left web");
        //    SpiderBehavior spider;
        //    spider = collision.gameObject.GetComponent<SpiderBehavior>();
        //}
    }
}

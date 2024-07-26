using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpiderWebBehavior : MonoBehaviour
{
    public float slowModifier = .2f;
    private PlayerBehavior player;
    private float playerBaseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        playerBaseSpeed = player.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.speed == playerBaseSpeed) player.speed *= slowModifier;
            //Debug.Log($"Player Speed is now {player.speed}");
        }
    }

    private void OnCollisionExit(Collision collision)
    { 
        if (collision.gameObject.tag == "Player")
        {
            player.speed = playerBaseSpeed;
            //Debug.Log($"Player speed reset to {player.speed}");
        }
    }
}

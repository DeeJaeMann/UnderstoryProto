using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBehavior : MonoBehaviour
{
    private GameBehavior gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player is home");
            gameManager.isHome = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player left home");
            gameManager.isHome = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    private int _foodCollected = 0;
    private int _playerHP = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Food
    {
        get { return _foodCollected; }
        set
        {
            _foodCollected = value;
            Debug.Log($"Food: {_foodCollected}");
        }
    }

    public int PlayerHP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.Log($"PlayerHP: {_playerHP}");
        }
    }
}

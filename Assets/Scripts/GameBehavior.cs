using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    private int _foodCollected = 0;
    private int _playerHP = 10;

    public int maxFood = 3;
    public TMP_Text healthText;
    public TMP_Text foodText;
    public TMP_Text progressText;

    // Start is called before the first frame update
    void Start()
    {
        foodText.text += $"{_foodCollected} / {maxFood}";
        healthText.text += $" {_playerHP}";
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
            foodText.text = $"Food: {Food} of {maxFood}";

            if(_foodCollected >= maxFood)
            {
                progressText.text = "You've found all the food!";
            }
            else
            {
                progressText.text = $"Only {maxFood - _foodCollected} more!";
            }
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

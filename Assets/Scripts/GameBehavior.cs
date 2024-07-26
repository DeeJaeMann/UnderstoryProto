using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehavior : MonoBehaviour
{
    private int _foodCollected = 0;
    private int _playerHP = 10;

    public int maxFood = 3;
    public TMP_Text healthText;
    public TMP_Text foodText;
    public TMP_Text progressText;

    public bool isHome = false;
    public Button winButton;
    public Button lossButton;

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

    private void LateUpdate()
    {
        if(CheckWin())
        {
            winButton.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public int Food
    {
        get { return _foodCollected; }
        set
        {
            _foodCollected = value;
            Debug.Log($"Food: {_foodCollected}");
            foodText.text = $"Food: {Food} / {maxFood}";

            if(_foodCollected >= maxFood)
            {
                progressText.text = "Return home!";
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
            healthText.text = $"Health: {PlayerHP}";

            if(_playerHP <= 0)
            {
                progressText.text = "Oh no!";
                lossButton.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public bool CheckWin()
    {
        if(Food >= maxFood && isHome) return true;
        
        return false;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}

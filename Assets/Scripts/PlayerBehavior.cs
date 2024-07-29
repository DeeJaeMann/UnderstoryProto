using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody _playerRB;
    private float _movementX;
    private float _movementY;
    public float speed = 0.5f;
    public float rotationSpeed = 100f;
    public float maxSpeed = 3f;

    private GameBehavior _gameManager;

    private Dictionary<string, int> _enemyDict = new()
    {
        {"Water", 10 },
        {"Ant", 1},
        {"Spider", 5 },
        {"Frog", 10 },
    };

    // Start is called before the first frame update
    private void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Rotate the player
        float rotation = _movementX * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0);

        //Vector3 movement = new(movementY, 0.0f, movementX);
        // Move the player forward and backward
        Vector3 movement = transform.forward * _movementY * speed;
        _playerRB.AddForce(movement * speed, ForceMode.VelocityChange);

        // Clamp the velocity to the maximum speed
        _playerRB.velocity = Vector3.ClampMagnitude(_playerRB.velocity, maxSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_enemyDict.ContainsKey(collision.gameObject.tag))
        {
            Debug.Log($"{collision.gameObject.tag} hit player!");
            int damage = _enemyDict[collision.gameObject.tag];
            Debug.Log($"Damage {damage}");
            _gameManager.PlayerHP -= damage;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody playerRB;
    private float movementX;
    private float movementY;
    public float speed = 0.5f;
    public float rotationSpeed = 100f;
    public float maxSpeed = 3f;
    // Start is called before the first frame update
    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Rotate the player
        float rotation = movementX * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0);

        //Vector3 movement = new(movementY, 0.0f, movementX);
        // Move the player forward and backward
        Vector3 movement = transform.forward * movementY * speed;
        playerRB.AddForce(movement * speed, ForceMode.VelocityChange);

        // Clamp the velocity to the maximum speed
        playerRB.velocity = Vector3.ClampMagnitude(playerRB.velocity, maxSpeed);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
}

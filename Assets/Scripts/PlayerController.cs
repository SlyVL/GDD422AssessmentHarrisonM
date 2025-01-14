using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    // Creating borders on the map so the player cannot move outside it
    float xMin = -130;
    float xMax = 130f;
    float yMin = -400f;
    float yMax = 400f;

    // the player movement speed which can be changed on the inspector 
    [SerializeField] float speed = 10f;

    // Reference to the player's Rigidbody2D component
    private Rigidbody2D rb;

    private void Start()
    {
        // Getting the Rigidbody2D component that is already attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Allows me to put lines of code elsewhere in a different private void to help free up the update function
        Moving();

        Animation();
    }

    private void Moving()
    {
        // Retrieving the player input for horizontal movement
        var xInput = Input.GetAxis("Horizontal") * speed;
        var yInput = Input.GetAxis("Vertical") * speed;

        // Creating the movement vector
        Vector2 movement = new Vector2(xInput, yInput);

        // Applying the movement to the Rigidbody2D by setting its velocity
        rb.velocity = movement;

        // Flip the sprite horizontally based on the direction of movement
        if (xInput < 0)  // Moving left
        {
            // Flip the sprite by setting flipX to true
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (xInput > 0)  // Moving right
        {
            // Reset the sprite's flipX to false
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // Restricting the player's position within the defined borders
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            Mathf.Clamp(rb.position.y, yMin, yMax)
        );

        // Updating the Rigidbody2D position (this prevents direct manipulation of position)
        rb.position = clampedPosition;
    }



    private void Animation()
    {
        // Calculate the movement magnitude (speed) and pass it to the animator as a float
        float speedMagnitude = new Vector2(rb.velocity.x, rb.velocity.y).magnitude;

        // Set the "Speed" parameter in the animator to the magnitude of the movement
        animator.SetFloat("Speed", speedMagnitude);

        // If you want to use a boolean instead of float:
        // bool isMoving = speedMagnitude > 0f;
        // animator.SetBool("isMoving", isMoving);
    }

}

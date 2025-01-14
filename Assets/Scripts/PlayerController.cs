using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Creating borders on the map so the player cannot move outside it
    float xMin = -130;
    float xMax = 130f;
    float yMin = -400f;
    float yMax = 400f;

    //the player movement speed which can be changed on the inspector 
    [SerializeField] float speed = 10f;

    // Reference to the player's Rigidbody2D component
    private Rigidbody2D rb;

    private void Start()
    {
        // Getting the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Allows me to put lines of code else where in a different private void
        Moving();
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

        // Restricting the player's position within the defined borders
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            Mathf.Clamp(rb.position.y, yMin, yMax)
        );

        // Updating the Rigidbody2D position (this prevents direct manipulation of position)
        rb.position = clampedPosition;
    }
}

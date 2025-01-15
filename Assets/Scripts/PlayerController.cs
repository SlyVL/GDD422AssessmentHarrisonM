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
    //This is to reference the trail renderer
    [SerializeField] private TrailRenderer tr;


    //All variables for the dash ability
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 600f;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 0f;

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

        Dashing();

        
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
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
            //CHATGPT FIX
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // Restricting the player's movement within the defined borders
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            Mathf.Clamp(rb.position.y, yMin, yMax)
        );

        // Updating the Rigidbody2D position, this allows my sprite to flip and update without any clipping through walls
        rb.position = clampedPosition;
    }
    //setting the keybind for the dash function
    private void Dashing()
    {
       if(isDashing)
        {
            return;
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    

    //CHATGPT FIX
    private void Animation()
    {
        // calculated the speed and passes it as a float 
        float speedMagnitude = new Vector2(rb.velocity.x, rb.velocity.y).magnitude;

        
        animator.SetFloat("Speed", speedMagnitude);

        
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // Capture player input directly for the dash direction
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        Vector2 dashDirection = new Vector2(xInput, yInput).normalized;

        // Default to the last movement direction if no input is detected
        if (dashDirection == Vector2.zero)
        {
            dashDirection = rb.velocity.normalized;
            if (dashDirection == Vector2.zero) // If still zero, default to right
            {
                dashDirection = Vector2.right;
            }
        }

        // Apply dash velocity
        rb.velocity = dashDirection * dashingPower;

        // Enable trail effect
        tr.emitting = true;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashingTime);

        // Stop dash movement by resetting velocity so there is no carried momentum
        rb.velocity = Vector2.zero;

        // Disable trail renderer
        tr.emitting = false;

        // End dash state
        isDashing = false;

        // Wait for cooldown before allowing dashing again
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }




}

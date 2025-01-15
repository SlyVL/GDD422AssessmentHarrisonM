using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    // Creating borders on the map so the player cannot move outside it
    float xMin = -130;
    float xMax = 130f;
    float yMin = -400f;
    float yMax = 400f;

    //player health 
    [SerializeField] float health = 1f;

    // the player movement speed which can be changed on the inspector 
    [SerializeField] float speed = 10f;

    //This is to reference the trail renderer
    [SerializeField] private TrailRenderer tr;

    //All variables for the dash ability
    private bool canDash = true;
    private bool isDashing;
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

    // Use OnTriggerEnter2D for 2D colliders
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Call the function to handle the player's death
            Die();
        }
    }

    void Die()
    {
        // Log for debugging
        Debug.Log("Player died. Reloading the scene immediately.");

        // Disable the player object (so it doesn't interfere with the scene reload)
        gameObject.SetActive(false);

        // Immediately reload the scene after the player dies
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
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
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (xInput > 0)  // Moving right
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // Restricting the player's movement within the defined borders
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            Mathf.Clamp(rb.position.y, yMin, yMax)
        );

        // Updating the Rigidbody2D position
        rb.position = clampedPosition;
    }

    private void Dashing()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void Animation()
    {
        // Calculate the speed and pass it as a float to the animator
        float speedMagnitude = new Vector2(rb.velocity.x, rb.velocity.y).magnitude;
        animator.SetFloat("Speed", speedMagnitude);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // Set IsDashing to true to trigger the dash animation
        animator.SetBool("IsDashing", true);

        // Double the player's speed during the dash
        float originalSpeed = speed;
        speed *= 2;

        // Enable trail effect
        tr.emitting = true;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashingTime);

        // Reset the speed to its original value
        speed = originalSpeed;

        // Disable trail effect
        tr.emitting = false;

        // End dash state
        isDashing = false;

        // Stop the dash animation
        animator.SetBool("IsDashing", false);

        // Wait for cooldown before allowing you to dash again
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

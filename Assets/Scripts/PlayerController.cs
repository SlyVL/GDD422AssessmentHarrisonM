using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameOverScreen gameOverScreen; // Reference to the GameOverScreen

    public Animator animator;
    private float xMin = -130, xMax = 130f, yMin = -400f, yMax = 400f;
    [SerializeField] float health = 1f;
    [SerializeField] float speed = 10f;
    [SerializeField] private TrailRenderer tr;

    private bool canDash = true, isDashing;
    private float dashingTime = 0.5f, dashingCooldown = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get Rigidbody2D component
    }

    void Update()
    {
        // Handle movement, animation, and dashing
        Moving();
        Animation();
        Dashing();
    }

    void FixedUpdate()
    {
        if (isDashing) return; // If dashing, do not allow movement
    }

    // On collision with enemy, player dies
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Die();  // Player dies on collision with an enemy
        }
    }

    void Die()
    {
        // Log death for debugging
        Debug.Log("Player died. Showing Game Over screen.");

        // Disable the player object so it doesn't interfere with the game
        gameObject.SetActive(false);

        // Show the Game Over screen
        gameOverScreen.Setup();

        // Wait for a short period before restarting the scene
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        // Wait for 2 seconds to show the Game Over screen
        yield return new WaitForSeconds(2f);

        // Reload the current scene after the delay
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Moving()
    {
        // Retrieve player input for movement
        var xInput = Input.GetAxis("Horizontal") * speed;
        var yInput = Input.GetAxis("Vertical") * speed;
        Vector2 movement = new Vector2(xInput, yInput);

        // Set Rigidbody2D velocity to move the player
        rb.velocity = movement;

        // Flip sprite based on movement direction
        if (xInput < 0) GetComponent<SpriteRenderer>().flipX = true;
        else if (xInput > 0) GetComponent<SpriteRenderer>().flipX = false;

        // Restrict player movement within boundaries
        Vector2 clampedPosition = new Vector2(Mathf.Clamp(rb.position.x, xMin, xMax), Mathf.Clamp(rb.position.y, yMin, yMax));
        rb.position = clampedPosition;
    }

    private void Dashing()
    {
        // Handle dashing mechanic
        if (isDashing) return;

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void Animation()
    {
        // Pass player speed to the Animator for smooth animation
        float speedMagnitude = new Vector2(rb.velocity.x, rb.velocity.y).magnitude;
        animator.SetFloat("Speed", speedMagnitude);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("IsDashing", true);

        // Double speed while dashing
        float originalSpeed = speed;
        speed *= 2;

        tr.emitting = true;  // Enable dash trail effect
        yield return new WaitForSeconds(dashingTime);

        speed = originalSpeed;  // Reset speed after dash
        tr.emitting = false;  // Disable dash trail effect
        isDashing = false;

        animator.SetBool("IsDashing", false);

        // Cooldown before allowing another dash
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

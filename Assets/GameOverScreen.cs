using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText; // Reference to the UI Text component that shows points

    void Start()
    {
        // Ensure the Game Over screen is hidden when the game starts
        gameObject.SetActive(false); // Hide the Game Over screen initially
    }

    public void Setup()
    {
        // Display the collected gems on the Game Over screen
        int totalGems = GameManager.Instance.gemsCollected;
        gameObject.SetActive(true);  // Enable the Game Over screen
        pointsText.text = totalGems + " Gems Collected";  // Update the UI text
    }

    private void Update()
    {
        // Wait for a key press to hide the Game Over screen (example: pressing Enter)
        if (Input.GetKeyDown(KeyCode.Return))  // You can change this to any key you prefer
        {
            gameObject.SetActive(false);  // Hide the Game Over screen when Enter is pressed
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }
}

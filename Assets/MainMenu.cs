using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This method is for quitting the game (will only work in a built game)
    public void Quit()
    {
        Application.Quit();
    }

    // This method will be called when the "Start" button is clicked
    public void StartGame()
    {
        // Load the Game scene when the "Start" button is clicked
        SceneManager.LoadScene("Game"); // Replace "Game" with the name of your game scene
    }
}

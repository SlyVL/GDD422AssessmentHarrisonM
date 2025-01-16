using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{

    

    private CoinPickup soundManager; // Reference to the CoinSoundManager

    
    void Start()
    {
        // Find the CoinSoundManager in the scene
        soundManager = FindObjectOfType<CoinPickup>();

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the sound effect
            soundManager.PlayCoinSound();

            // Call the function to collect the gem
            Collect();
        }
    }

    void Collect()
    {
        // Increment the gem count
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddGem();
        }

        // Disables the object when collected
        gameObject.SetActive(false);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public AudioClip[] coinSounds; // Array to hold coin sound effects
    private AudioSource audioSource; // Reference to the AudioSource
    private int coinCount = 0; // Player's coin count

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCoinSound()
    {
        // Determine which sound to play based on coin count
        AudioClip soundToPlay;
        if (coinCount < 7)
        {
            soundToPlay = coinSounds[0]; // Early coins
        }
        else if (coinCount < 17)
        {
            soundToPlay = coinSounds[1]; // Mid-range coins
        }
        else if (coinCount < 27)
        {
            soundToPlay = coinSounds[2];
        }
        else if (coinCount < 37)
        {
            soundToPlay= coinSounds[3];
        }
        else
        {
            soundToPlay = coinSounds[4]; //final coin as a victory sound too
        }

        // Play the selected sound
        audioSource.PlayOneShot(soundToPlay);

        // Increment the coin count
        coinCount++;
    }
}


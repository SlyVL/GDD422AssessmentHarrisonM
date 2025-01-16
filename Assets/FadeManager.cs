using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;  // Singleton instance
    public Image fadeImage; // UI Image component for fading effect
    public float fadeDuration = 1f; // Duration of the fade transition

    private void Awake()
    {
        // Ensure that there is only one instance of FadeManager in the scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading a new scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this to fade out and load a new scene
    public void FadeOut(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Start fading out (fade to black)
        float timeElapsed = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (timeElapsed < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, targetColor, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor;

        // Load the new scene after fade out is complete
        SceneManager.LoadScene(sceneName);
    }

    // Call this to fade in (if you need it for transitions back to the main menu, for example)
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float timeElapsed = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (timeElapsed < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, targetColor, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor;
    }
}

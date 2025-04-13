using UnityEngine;
using UnityEngine.UI;
using System.Collections;  // <-- This is the important line to add

public class FadeInOut : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    private bool isFading = false;

    private void Start()
    {
        // Set the image to black at the start
        fadeImage.color = new Color(0, 0, 0, 1);

        // Wait for user interaction before fading in
        StartCoroutine(WaitForInteraction());
    }

    private IEnumerator WaitForInteraction()
    {
        // Wait until the user clicks anywhere in the game window
        yield return new WaitUntil(() => Input.anyKeyDown);

        // Start fade-in after the first interaction
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        isFading = true;
        float timeElapsed = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, endAlpha);

        while (timeElapsed < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, endColor, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = endColor;
        isFading = false;
    }
}


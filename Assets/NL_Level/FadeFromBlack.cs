using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeFromBlack : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);

            // Make sure it starts fully black
            fadeImage.color = new Color(0f, 0f, 0f, 1f);

            // Begin fading out
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        // Final cleanup
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        fadeImage.gameObject.SetActive(false);
    }
}

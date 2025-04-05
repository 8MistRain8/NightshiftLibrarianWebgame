using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage;
    public float duration = 2f;

    void Start()
    {
        StartCoroutine(FadeFromBlack());
    }

    System.Collections.IEnumerator FadeFromBlack()
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, t / duration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // optional: disable image after fade
    }
}

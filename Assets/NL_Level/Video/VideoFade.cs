using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class VideoFadeAdditive : MonoBehaviour
{
    [Header("References")]
    public VideoPlayer videoPlayer;   // Your VideoPlayer
    public RawImage rawImage;         // RawImage showing the video

    [Header("Fade Settings")]
    public float fadeDuration = 1f;   // seconds to fade in/out
    [Range(0f, 1f)]
    public float maxAlpha = 1f;       // maximum brightness contribution

    private Material mat;

    private void Awake()
    {
        // Get the material from the RawImage
        mat = rawImage.material;

        // Start fully invisible
        SetAlpha(0.0f);
    }

    /// <summary>
    /// Fade in and play the video
    /// </summary>
    public void PlayVideoWithFade()
    {
        videoPlayer.Play();
        StartCoroutine(FadeAlpha(0f, maxAlpha, fadeDuration));
    }

    /// <summary>
    /// Fade out and stop the video
    /// </summary>
    public void StopVideoWithFade()
    {
        StartCoroutine(FadeAlpha(mat.GetFloat("_Alpha"), 0f, fadeDuration, stopVideo: true));
    }

    private IEnumerator FadeAlpha(float from, float to, float duration, bool stopVideo = false)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, t / duration);

            // Ensure a tiny minimum so additive never disappears completely (optional)
            alpha = Mathf.Max(alpha, 0f);

            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(to);

        if (stopVideo)
            videoPlayer.Stop();
    }

    private void SetAlpha(float a)
    {
        if (mat != null)
            mat.SetFloat("_Alpha", a);
    }
}

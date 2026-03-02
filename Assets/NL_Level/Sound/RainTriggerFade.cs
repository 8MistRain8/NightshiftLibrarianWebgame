using UnityEngine;
using System.Collections;

public class RainTriggerFade : MonoBehaviour
{
    public AudioSource rainAudio;
    public float fadeDuration = 2f;
    public float maxVolume = 0.7f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!rainAudio.isPlaying)
                rainAudio.Play();
            StartCoroutine(FadeAudio(rainAudio, 0f, maxVolume, fadeDuration));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeAudio(rainAudio, rainAudio.volume, 0f, fadeDuration, stopAfterFade: true));
        }
    }

    private IEnumerator FadeAudio(AudioSource source, float from, float to, float duration, bool stopAfterFade = false)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        source.volume = to;
        if (stopAfterFade)
            source.Stop();
    }
}

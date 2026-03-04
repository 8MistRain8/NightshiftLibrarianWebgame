using UnityEngine;
using System.Collections;

public class RainTriggerFade : MonoBehaviour
{
    public AudioSource rainAudio;
    public float fadeDuration = 2f;
    public float maxVolume = 0.7f;

    private static int activeZones = 0;
    private static Coroutine fadeRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        activeZones++;

        if (!rainAudio.isPlaying)
        {
            rainAudio.volume = 0f;
            rainAudio.Play();
        }

        StartFade(maxVolume);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        activeZones--;

        if (activeZones <= 0)
        {
            activeZones = 0;
            StartFade(0f, stopAfter: true);
        }
    }

    void StartFade(float target, bool stopAfter = false)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeAudio(target, stopAfter));
    }

    IEnumerator FadeAudio(float target, bool stopAfter)
    {
        float start = rainAudio.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            rainAudio.volume = Mathf.Lerp(start, target, time / fadeDuration);
            yield return null;
        }

        rainAudio.volume = target;

        if (stopAfter && target == 0f)
            rainAudio.Stop();
    }
}
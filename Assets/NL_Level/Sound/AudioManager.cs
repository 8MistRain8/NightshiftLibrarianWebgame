using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource currentSource;
    private AudioSource nextSource;

    [Header("Crossfade Settings")]
    public float fadeInDuration = 1f;   // fast fade-in
    public float fadeOutDuration = 5f;  // slow fade-out

    private Coroutine fadeCoroutineCurrent;
    private Coroutine fadeCoroutineNext;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

        currentSource = gameObject.AddComponent<AudioSource>();
        currentSource.loop = true;

        nextSource = gameObject.AddComponent<AudioSource>();
        nextSource.loop = true;
    }

    /// <summary>
    /// Play a music clip with fast fade-in
    /// </summary>
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (fadeCoroutineNext != null) StopCoroutine(fadeCoroutineNext);
        if (fadeCoroutineCurrent != null) StopCoroutine(fadeCoroutineCurrent);

        StartCoroutine(CrossfadeMusic(clip));
    }

    private IEnumerator CrossfadeMusic(AudioClip newClip)
    {
        if (currentSource.clip == newClip)
        {
            currentSource.Stop(); // restart if same clip
        }

        nextSource.clip = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        fadeCoroutineCurrent = StartCoroutine(FadeVolume(currentSource, currentSource.volume, 0f, fadeOutDuration)); // slow fade-out
        fadeCoroutineNext = StartCoroutine(FadeVolume(nextSource, 0f, 1f, fadeInDuration)); // fast fade-in

        yield return new WaitForSeconds(Mathf.Max(fadeInDuration, fadeOutDuration));

        // Swap sources
        AudioSource temp = currentSource;
        currentSource = nextSource;
        nextSource = temp;

        nextSource.Stop();
        fadeCoroutineCurrent = null;
        fadeCoroutineNext = null;
    }

    /// <summary>
    /// Stop music with slow fade-out
    /// </summary>
    public void StopMusic()
    {
        if (fadeCoroutineCurrent != null) StopCoroutine(fadeCoroutineCurrent);
        fadeCoroutineCurrent = StartCoroutine(FadeOut(currentSource, fadeOutDuration));
    }

    private IEnumerator FadeOut(AudioSource source, float duration)
    {
        if (source == null || !source.isPlaying) yield break;

        yield return FadeVolume(source, source.volume, 0f, duration);

        source.Stop();
        source.volume = 1f;
        source.clip = null;
        fadeCoroutineCurrent = null;
    }

    /// <summary>
    /// Smoothly interpolate volume using SmoothStep
    /// </summary>
    private IEnumerator FadeVolume(AudioSource source, float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / duration);
            float smoothed = Mathf.SmoothStep(0f, 1f, normalized);
            source.volume = Mathf.Lerp(from, to, smoothed);
            yield return null;
        }
        source.volume = to;
    }

    /// <summary>
    /// Returns the AudioSource currently playing music (for random volume modulation)
    /// </summary>
    public AudioSource GetCurrentSource()
    {
        return currentSource;
    }
}
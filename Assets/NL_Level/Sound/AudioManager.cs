using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource currentSource;
    private AudioSource nextSource;

    [Header("Crossfade Settings")]
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 5f;

    [Range(0f, 1f)]
    public float musicMaxVolume = 0.6f; // << THIS controls peak volume

    private Coroutine fadeCoroutineCurrent;
    private Coroutine fadeCoroutineNext;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

        currentSource = gameObject.AddComponent<AudioSource>();
        currentSource.loop = true;
        currentSource.volume = 0f;

        nextSource = gameObject.AddComponent<AudioSource>();
        nextSource.loop = true;
        nextSource.volume = 0f;
    }

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
            yield break; // no restart if same clip

        nextSource.clip = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        fadeCoroutineCurrent = StartCoroutine(FadeVolume(currentSource, currentSource.volume, 0f, fadeOutDuration));
        fadeCoroutineNext = StartCoroutine(FadeVolume(nextSource, 0f, musicMaxVolume, fadeInDuration));

        yield return new WaitForSeconds(Mathf.Max(fadeInDuration, fadeOutDuration));

        AudioSource temp = currentSource;
        currentSource = nextSource;
        nextSource = temp;

        nextSource.Stop();
        nextSource.clip = null;

        fadeCoroutineCurrent = null;
        fadeCoroutineNext = null;
    }

    public void StopMusic()
    {
        if (fadeCoroutineCurrent != null)
            StopCoroutine(fadeCoroutineCurrent);

        fadeCoroutineCurrent = StartCoroutine(FadeOut(currentSource, fadeOutDuration));
    }

    private IEnumerator FadeOut(AudioSource source, float duration)
    {
        if (source == null || !source.isPlaying) yield break;

        yield return FadeVolume(source, source.volume, 0f, duration);

        source.Stop();
        source.volume = 0f; // << do NOT reset to 1
        source.clip = null;

        fadeCoroutineCurrent = null;
    }

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

    public AudioSource GetCurrentSource()
    {
        return currentSource;
    }
}
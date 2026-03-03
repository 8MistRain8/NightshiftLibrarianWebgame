using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoTrigger : MonoBehaviour
{
    [Header("Video Setup")]
    public VideoPlayer videoPlayer;
    public VideoFadeAdditive videoFade;

    private Collider triggerCollider;

    private void Awake()
    {
        // Cache collider reference for enabling/disabling
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerCollider.enabled) return; // Only active if collider is enabled

        if (other.CompareTag("Player"))
        {
            if (VideoManager.Instance != null)
            {
                VideoManager.Instance.RequestPlay(this);
            }
            else
            {
                Debug.LogError("VideoManager not found in scene!");
            }
        }
    }

    // Called by VideoManager when this trigger should play
    public void PlayVideo()
    {
        StartCoroutine(PrepareAndPlay());
    }

    private IEnumerator PrepareAndPlay()
    {
        // Reset video
        videoPlayer.Stop();
        videoPlayer.time = 0;

        // Prepare video
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
            yield return null;

        // Start fade in and playback
        videoFade.PlayVideoWithFade();
    }

    // Trigger fade-out; fade script stops the video after fade completes
    public void StopVideo()
    {
        videoFade.StopVideoWithFade();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopVideo();
        }
    }

    /// <summary>
    /// Enable or disable this trigger, used by KeyPickup
    /// </summary>
    public void SetActiveTrigger(bool active)
    {
        if (triggerCollider != null)
            triggerCollider.enabled = active;
    }
}
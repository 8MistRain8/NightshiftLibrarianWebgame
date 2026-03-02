using UnityEngine;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;      // Assign your VideoPlayer here
    public VideoFadeAdditive videoFade;  // Reference to your fade script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Play();
            videoFade.PlayVideoWithFade(); // Fade in additive video
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            videoFade.StopVideoWithFade(); // Fade out and stop video
        }
    }
}

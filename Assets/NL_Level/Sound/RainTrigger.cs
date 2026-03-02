using UnityEngine;

public class RainTrigger : MonoBehaviour
{
    public AudioSource rainAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!rainAudio.isPlaying)
                rainAudio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rainAudio.isPlaying)
                rainAudio.Stop();
        }
    }
}

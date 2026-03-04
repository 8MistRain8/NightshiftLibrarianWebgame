using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioClip musicClip;

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (playerInside) return;

        playerInside = true;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(musicClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic();
        }
    }
}
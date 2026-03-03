using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MusicZone : MonoBehaviour
{
    [Header("Zone Music")]
    public AudioClip zoneMusic;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float volume = 1f; // Fixed volume for this zone

    private int collidersInside = 0;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (!col.isTrigger) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        collidersInside++;
        if (collidersInside == 1)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMusic(zoneMusic);
                AudioManager.Instance.GetCurrentSource().volume = volume;
            }
            else Debug.LogError("AudioManager not found!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        collidersInside--;
        if (collidersInside <= 0)
        {
            collidersInside = 0;

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.StopMusic();
            }
        }
    }
}
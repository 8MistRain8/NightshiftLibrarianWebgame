using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Door door;                   // drag your Door here
    public VideoTrigger[] triggersToEnable; // drag your two VideoTrigger areas here

    private AudioSource audioSource;
    private bool pickedUp = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Make sure triggers start disabled
        foreach (var trigger in triggersToEnable)
            trigger.SetActiveTrigger(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;

        if (other.CompareTag("Player"))
        {
            pickedUp = true;

            audioSource.Play();

            door.OpenDoor();

            // Enable the video triggers
            foreach (var trigger in triggersToEnable)
                trigger.SetActiveTrigger(true);

            // Hide the key visually
            GetComponent<MeshRenderer>().enabled = false;

            // Destroy after sound finishes
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
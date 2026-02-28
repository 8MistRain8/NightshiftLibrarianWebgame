using UnityEngine;
public class KeyPickup : MonoBehaviour
{
    public Door door;        // drag your Door here in inspector
    private AudioSource audioSource;
    private bool pickedUp = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;

        if (other.CompareTag("Player"))
        {
            pickedUp = true;

            audioSource.Play();

            door.OpenDoor();

            // hide the key visually
            GetComponent<MeshRenderer>().enabled = false;

            // destroy after sound finishes
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
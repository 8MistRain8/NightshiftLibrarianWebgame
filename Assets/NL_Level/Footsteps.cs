using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]
public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f;
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);
    public Vector2 volumeRange = new Vector2(0.8f, 1f);

    private AudioSource audioSource;
    private CharacterController controller;
    private float stepTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Only play footsteps when grounded and moving
        if (controller.isGrounded && controller.velocity.magnitude > 0.2f)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        int index = Random.Range(0, footstepClips.Length);
        AudioClip clip = footstepClips[index];

        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        audioSource.volume = Random.Range(volumeRange.x, volumeRange.y);
        audioSource.PlayOneShot(clip);
    }
}

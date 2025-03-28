using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;

    private float verticalRotation = 0;
    public float upDownRange = 90.0f;

    private CharacterController characterController;

    // Gravity settings
    public float gravity = -9.8f;  // Default gravity
    private Vector3 velocity; // For gravity

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Movement
        float forwardSpeed = Input.GetAxis("Vertical") * speed;
        float sideSpeed = Input.GetAxis("Horizontal") * speed;

        Vector3 speedVector = new Vector3(sideSpeed, velocity.y, forwardSpeed);
        speedVector = transform.rotation * speedVector;

        // Apply gravity
        if (characterController.isGrounded)
        {
            velocity.y = -2f; // Small value to keep the player grounded
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // Apply gravity
        }

        // Move the character
        characterController.Move(speedVector * Time.deltaTime);
    }
}

using UnityEngine;
using TMPro; // For TextMeshPro

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2.0f; // How close you need to be to see the text
    public TMP_Text subtitleText; // Reference to the TextMeshPro UI Text element to display the subtitle
    private GameObject currentInteractableObject;

    void Update()
    {
        // Check if there is an interactable object within range
        CheckForInteractableObject();
    }

    void CheckForInteractableObject()
    {
        RaycastHit hit;

        // Raycast to find objects in front of the player
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            // Check if the object is interactable
            if (hit.collider.CompareTag("Interactable"))
            {
                currentInteractableObject = hit.collider.gameObject;
                // Display the interact text (or subtitle) from the interactable object
                subtitleText.text = hit.collider.GetComponent<InteractableObject>().interactText;
            }
            else
            {
                currentInteractableObject = null;
                subtitleText.text = ""; // Clear text when no interactable object is nearby
            }
        }
        else
        {
            currentInteractableObject = null;
            subtitleText.text = ""; // Clear text when nothing is hit
        }
    }
}


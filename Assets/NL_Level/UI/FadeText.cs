using UnityEngine;
using UnityEngine.UIElements;

public class FadeText : MonoBehaviour
{
    private Label hitF11Label;
    public float fadeDuration = 5f; // Make this public to set from the Inspector
    private float fadeTimer;

    void Start()
    {
        // Get the UIDocument component from this GameObject
        var uiDocument = GetComponent<UIDocument>();

        // Get the root visual element (the root of the UXML hierarchy)
        var root = uiDocument.rootVisualElement;

        // Find the Label by its name in the UXML
        hitF11Label = root.Q<Label>("hitF11Label");

        // Check if the label was found
        if (hitF11Label == null)
        {
            Debug.LogError("Label 'hitF11Label' not found in the UXML. Please ensure it exists in the UI Builder.");
            return; // Exit if the label is not found
        }

        // Set the label to be fully visible at the start
        hitF11Label.style.opacity = 1f;

        // Set the fade timer to start the fade duration
        fadeTimer = fadeDuration;
    }

    void Update()
    {
        // Fade out the label over time
        if (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, 1 - (fadeTimer / fadeDuration)); // Calculate the alpha based on the timer
            hitF11Label.style.opacity = alpha; // Update the label's opacity
        }
    }
}


using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float deltaTime = 0.0f;

    void Start()
    {
        // Cap the frame rate to 60 FPS
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Calculate the time between frames
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        // Display the FPS counter in the top left corner
        int fps = Mathf.CeilToInt(1.0f / deltaTime);
        GUI.Label(new Rect(10, 10, 100, 50), "FPS: " + fps);
    }
}

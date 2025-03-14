using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PixelationController : MonoBehaviour
{
    public Material pixelationMaterial;  // Material with the Pixelation Shader
    [Range(1, 64)] public int pixelSize = 8;  // Default pixel size
    private Camera mainCamera;  // Main camera to apply post-process effect

    void Start()
    {
        mainCamera = GetComponent<Camera>();

        if (pixelationMaterial != null)
        {
            // Set initial pixelation level
            pixelationMaterial.SetFloat("_PixelSize", pixelSize);
        }
        else
        {
            Debug.LogError("Pixelation material is not assigned!");
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Apply the pixelation material to the camera's output
        if (pixelationMaterial != null)
        {
            pixelationMaterial.SetFloat("_PixelSize", pixelSize);
            Graphics.Blit(src, dest, pixelationMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);  // If no pixelation material, just pass through the image
        }
    }

    void Update()
    {
        // Optionally, adjust the pixelation level during gameplay or via UI
        if (pixelationMaterial != null)
        {
            pixelationMaterial.SetFloat("_PixelSize", pixelSize);
        }
    }

    // Function to increase pixelation (called via UI or other script)
    public void IncreasePixelation()
    {
        pixelSize = Mathf.Min(pixelSize + 1, 64); // Increase but limit max to 64
    }

    // Function to decrease pixelation (called via UI or other script)
    public void DecreasePixelation()
    {
        pixelSize = Mathf.Max(pixelSize - 1, 1); // Decrease but limit min to 1
    }
}

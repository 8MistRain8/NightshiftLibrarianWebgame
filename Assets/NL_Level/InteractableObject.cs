using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string interactText = "This is a subtitle!"; // The text that will show when the player is near this object

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f); // visual aid for range (2 meters radius)
    }
}

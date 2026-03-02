using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    public Door door; // Assign your Door script here

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (door != null)
                door.CloseDoor(); // Closes the door when player hits the collider
        }
    }
}

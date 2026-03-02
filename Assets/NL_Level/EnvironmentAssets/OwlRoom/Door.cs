using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject closedDoor;
    public GameObject openDoor;

    // Open the door (call this whenever you want)
    public void OpenDoor()
    {
        if (closedDoor != null)
            closedDoor.SetActive(false);

        if (openDoor != null)
            openDoor.SetActive(true);
    }

    // Close the door
    public void CloseDoor()
    {
        if (closedDoor != null)
            closedDoor.SetActive(true);

        if (openDoor != null)
            openDoor.SetActive(false);
    }
}

using UnityEngine;
using UltimateXR.Manipulation;

public class DoorDestroyer : MonoBehaviour
{
    public GameObject door; // Assign the door GameObject in the Inspector
    private UxrGrabbableObject grabbableObject;

    void Start()
    {
        grabbableObject = GetComponent<UxrGrabbableObject>();

        if (grabbableObject == null)
        {
            Debug.LogError("UxrGrabbableObject component is missing from this object.");
        }

        if (door == null)
        {
            Debug.LogError("Door GameObject is not assigned.");
        }
    }

    void Update()
    {
        // Check if the object is being grabbed
        if (grabbableObject != null && grabbableObject.IsBeingGrabbed)
        {
            Destroy(door);
        }
    }
}

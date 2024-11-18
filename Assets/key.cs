using UnityEngine;
using UltimateXR.Manipulation;

public class DoorDestroyer : MonoBehaviour
{
    public GameObject door; // Assign the door GameObject in the Inspector
    private UxrGrabbableObject grabbableObject;
    private GameObject painting; 

    void Start()
    {
        painting = GameObject.FindWithTag("painting");
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
        // if (grabbableObject != null && grabbableObject.IsBeingGrabbed)
        // {
        //     Destroy(door);
        // }
        Vector3 relativePosition = painting.transform.InverseTransformPoint(transform.position);

        // Check if A is on B's positive X-axis
        if (relativePosition.x > 1)
        {
            Destroy(door);
            // Debug.Log("Object A is on the positive X-axis of Object B!");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Debug.Log("Debug Collider!");
        // Check if the other GameObject has the tag "painting"
        if (other.gameObject.CompareTag("painting"))
        {
            Destroy(door);
            // Perform your custom logic here
        }
    }
}

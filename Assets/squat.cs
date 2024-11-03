using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;

public class SquatMechanic : MonoBehaviour
{
    public float squatHeight = 0.5f;      // Height when squatting
    public float normalHeight = 1.0f;     // Normal standing height
    public float squatSpeed = 5.0f;       // Speed of transition
    private bool isSquatting = false;     // To check squat status
    private Vector3 targetScale;          // Target scale to lerp to

    private void Start()
    {
        // Set the default height to normal standing height
        targetScale = transform.localScale;
    }

    private void Update()
    {
        // Check for squat input (e.g., left control key)
        if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Button1))
        {
            Debug.Log("Button1 press");
            isSquatting = true;
            targetScale = new Vector3(transform.localScale.x, squatHeight, transform.localScale.z);
        }
        else if (UxrAvatar.LocalAvatarInput.GetButtonsPressUp(UxrHandSide.Left, UxrInputButtons.Button1))
        {
            isSquatting = false;
            targetScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);
        }

        // Smoothly transition to the target scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * squatSpeed);
    }
}

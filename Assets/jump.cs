using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;

public class JumpScript : MonoBehaviour
{
    public float jumpForce = 5f;      // Adjustable jump force
    private bool isGrounded;          // Checks if the player is on the ground
    private Rigidbody rb;
    [SerializeField, Range(0f, 10f)] private float heightOffset;

    private void OnDestroy()
    {
        // UxrAvatar.AvatarUpdated -= UxrAvatarOnGlobalAvatarMoved;
    }

    // private void UxrAvatarOnGlobalAvatarMoved(object sender, UxrAvatarUpdateEventArgs e)
    // {
    //     UxrAvatar.LocalAvatar.transform.position = e.NewPosition + Vector3.up * heightOffset;
    // }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // UxrAvatar.AvatarMoved += UxrAvatarOnGlobalAvatarMoved;

        isGrounded = true;
    }

    void Update()
    {
        if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Button1) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Debug.Log("Try to jumo");
        rb.AddForce(Vector3.up * jumpForce * 3, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionStay()
    {
        // Check if we landed on the ground
        isGrounded = true;
    }
}

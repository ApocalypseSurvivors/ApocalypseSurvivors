using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;

public class JumpScript : MonoBehaviour
{
    public float jumpForce = 5f;      // Adjustable jump force
    private bool isGrounded;          // Checks if the player is on the ground
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
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

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we landed on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

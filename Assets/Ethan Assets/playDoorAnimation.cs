using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playDoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key")){
            myAnimationController.SetBool("isUnlocked", true);
        }
    }
}

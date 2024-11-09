using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerDoor1 : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            if (openTrigger)
            {
                myDoor.Play("open-door", 0, 0.0f);
            }
        }
    }
}

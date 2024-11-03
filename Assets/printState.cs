using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Mechanics.Weapons;

public class PrintUxrActorStatus : MonoBehaviour
{
    private UxrActor actor;
    private float oldLife;

    private void Start()
    {
        // Get the UxrActor component on this GameObject (or any specified GameObject)
        actor = GetComponent<UxrActor>();
        oldLife = actor.Life;

        // Check if the component exists before accessing it
        if (actor != null)
        {
            // PrintActorStatus();
        }
        else
        {
            Debug.LogError("UxrActor component not found on this GameObject.");
        }
    }

    private void Update() {
        if (oldLife != actor.Life) {
            PrintActorStatus();
            oldLife = actor.Life;
        }
    }

    private void PrintActorStatus()
    {
        // Print the public properties of the UxrActor component
        Debug.Log("Actor: IsDead: " + actor.IsDead);
        Debug.Log("Actor: Life: " + actor.Life);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UltimateXR.Mechanics.Weapons;

public class Witch : MonoBehaviour
{
    [SerializeField]
    int HP = 100;
    private Animator animator;
    private NavMeshAgent navAgent;
    private UxrActor actor;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        actor = GetComponent<UxrActor>();
        actor.Life = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.velocity.magnitude > 0.1f) {
            animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
        }
        
    }
}

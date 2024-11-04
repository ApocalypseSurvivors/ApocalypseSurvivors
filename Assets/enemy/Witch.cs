using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UltimateXR.Mechanics.Weapons;

public class Witch : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navAgent;
    private UxrActor actor;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        actor = GetComponent<UxrActor>();

        // actor.DamageReceived += HandleDamage;
    }

    void HandleDamage(object sender, UxrDamageEventArgs e) {
        if (actor.Life <= 0) {
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0) {
                animator.SetTrigger("Die1");
            } else {
                animator.SetTrigger("Die2");
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        navAgent.enabled = !actor.IsDead;
        if (navAgent.enabled && navAgent.velocity.magnitude > 0.1f) {
            animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
        }
        
    }

    private void OnDestroy()
    {
      actor.DamageReceived -= HandleDamage;
    }
}

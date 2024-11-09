using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UltimateXR.Mechanics.Weapons;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navAgent;
    private UxrActor actor;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        actor = GetComponent<UxrActor>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        // rb.isKinematic = true;   
        actor.DamageReceived += HandleDamage;
    }

    void HandleDamage(object sender, UxrDamageEventArgs e) {
        Debug.Log("Debug: Damage, actor life " + actor.Life);
        if (actor.IsDead) {
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0) {
                animator.SetTrigger("Die1");
            } else {
                animator.SetTrigger("Die2");
            }
        } else {
            // navAgent.enabled = false;              // Disable NavMeshAgent
            // rb.isKinematic = false;  
            if (e.DamageType == UxrDamageType.ProjectileHit) {
                Vector3 source = e.ActorSource.transform.position;
                Vector3 hitpoint = e.RaycastHit.point;
                if (rb == null) {
                    Debug.Log("Debug null rb ");
                }
                Debug.Log("Debug Force: " + (hitpoint - source).normalized * 100);
                rb.AddForceAtPosition((hitpoint - source).normalized * 100, e.RaycastHit.transform.position);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!actor.IsDead ) {
                Vector3 target = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
                if (target == null) {
                    Debug.Log("Debug null Player ");
                }
                Debug.Log("Debug set Player ");
                navAgent.destination = target;

                if (navAgent.velocity.magnitude > 0.1f) {
                    animator.SetBool("isWalking", true);
                } else {
                    animator.SetBool("isWalking", false);
                }
            if (rb.IsSleeping())
            {
                ReactivateNavMeshAgent();
            } else {
                // navAgent.enabled = false;              // Disable NavMeshAgent
            }
        } else {
            // animator.SetBool("isWalking", false);
            navAgent.enabled = false;
        }
        
    }
  private void ReactivateNavMeshAgent()
    {
        // Debug.Log("Debug ReactivateNavMeshAgent");
        // Re-enable NavMeshAgent and switch back to kinematic Rigidbody
        // rb.isKinematic = true;
        // navAgent.enabled = true;
    }

    private void OnDestroy()
    {
      actor.DamageReceived -= HandleDamage;
    }
}


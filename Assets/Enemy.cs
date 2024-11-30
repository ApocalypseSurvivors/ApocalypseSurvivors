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
    [SerializeField] float attackDamage = 10;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float bulletForceMultiplier = 10;
    [SerializeField] HealthBar healthBar;
    public GameObject bloodSprayEffect;
    private void awake() {
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        actor = GetComponent<UxrActor>();
        actor.Life = maxHealth;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.isKinematic = true;   
        actor.DamageReceived += HandleDamage;

        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(actor.Life, maxHealth); 
        // HideHealthBar();
        Invoke("HideHealthBar", 1); // Hide health bar after 2 seconds if no new damage is taken
    }

   void HideHealthBar()
   {
       healthBar.gameObject.SetActive(false);
   }

    void HandleDamage(object sender, UxrDamageEventArgs e) {
        float damage = e.Damage; 
        // Debug.Log("Debug: Damage, actor life " + actor.Life);
        if (healthBar != null) {
            healthBar.gameObject.SetActive(true); // Show health bar
            CancelInvoke(); // Cancel any previous HideHealthBar invocation
            Invoke("HideHealthBar", 2); // Hide health bar after 2 seconds if no new damage is taken
            healthBar.UpdateHealthBar(actor.Life, maxHealth); 
        } else {
            Debug.Log("Debug null healthBar");
        }
        if (e.Dies) {
            int randomValue = Random.Range(0, 2);
            // if (randomValue == 0) {
            //     animator.SetTrigger("Die1");
            // } else {
            //     animator.SetTrigger("Die2");
            // }
        } else {
            navAgent.enabled = false;              // Disable NavMeshAgent
            rb.isKinematic = false;  
            if (e.DamageType == UxrDamageType.ProjectileHit) {
                Vector3 source = e.ActorSource.transform.position;
                Vector3 hitpoint = e.RaycastHit.point;
                if (rb == null) {
                    // Debug.Log("Debug null rb ");
                }
                // Debug.Log("Debug Force: " + (hitpoint - source).normalized * 100);
                rb.AddForceAtPosition((hitpoint - source).normalized * bulletForceMultiplier, e.RaycastHit.transform.position);
                if (!dead())
                {
                    CreateBloodSprayEffect(hitpoint);
                }
            }
        }
    }

    private bool dead() {
        return actor.Life <= 0;
    }

    void CreateBloodSprayEffect(Vector3 hitpoint)
    {
        GameObject blood = Instantiate(
            bloodSprayEffect,
            hitpoint,
            Quaternion.LookRotation(hitpoint)
            );
    }


    // Update is called once per frame
    void Update()
    {
        if (!dead() ) {
                Vector3 target = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
                if (target == null) {
                    // Debug.Log("Debug null Player ");
                }
                // Debug.Log("Debug set Player ");
                navAgent.destination = target;

                // if (navAgent.velocity.magnitude > 0.1f) {
                //     animator.SetBool("isWalking", true);
                // } else {
                //     animator.SetBool("isWalking", false);
                // }
            if (rb.velocity.magnitude < 0.1)
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

    public void applyDamage() {
        Player player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        player.TakeDamage(attackDamage, transform);
        // Debug.Log("Debug apply");
    }

    private void ReactivateNavMeshAgent()
    {
        // Debug.Log("Debug ReactivateNavMeshAgent");
        // Re-enable NavMeshAgent and switch back to kinematic Rigidbody
        rb.isKinematic = true;
        navAgent.enabled = true;
    }

    private void OnDestroy()
    {
      actor.DamageReceived -= HandleDamage;
    }
}


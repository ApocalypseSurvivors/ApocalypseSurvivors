using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UltimateXR.Mechanics.Weapons;
using UltimateXR.Locomotion;

public class Player : MonoBehaviour
{
    private UxrActor actor;
    private Rigidbody rb;
    private PostProcessVolume vol;
    private Vignette vignette;
    [SerializeField] float maxHealth = 100;
    [SerializeField] PlayerHealthBar healthBar;
    public float test_hp;


    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<UxrActor>();
        actor.Life = maxHealth;

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        healthBar = GetComponentInChildren<PlayerHealthBar>();
        vol = GetComponentInChildren<PostProcessVolume>();
        vol.profile.TryGetSettings<Vignette>(out vignette);
        if (!vignette) {
            Debug.Log("Debug null vignette");
        } else {
            vignette.enabled.Override(false);
        }
        
        if (healthBar != null) {  
            UpdateHealthBar();
        } else {
            Debug.Log("Debug null healthBar 1");
        }
 
    }

    private IEnumerator TakeDamageEffect()
    {
        float intensity = 0.4f;

        // Enable the vignette and set initial intensity
        vignette.enabled.Override(true);
        vignette.intensity.Override(0.4f);

        // Debug.Log("Debug enable vigne");
        // Wait for the initial delay
        yield return new WaitForSeconds(0.4f);

        // Gradually reduce the vignette intensity
        while (intensity > 0)
        {
            intensity -= 0.01f;

            // Ensure intensity does not go below zero
            if (intensity < 0) intensity = 0;

            // Update the vignette intensity
            vignette.intensity.Override(intensity);

            // Wait for a short delay before the next update
            yield return new WaitForSeconds(0.1f);
        }

        // Disable the vignette
        vignette.enabled.Override(false);

        // End the coroutine
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        test_hp = actor.Life;
        float time = Time.deltaTime;
        if (actor.Life > 10) {
            TakeDamage(time / 5);
        }
        if (healthBar != null) { 
            UpdateHealthBar();
        } else {
            Debug.Log("Debug null healthBar 2");
        }
    }

    void UpdateHealthBar() {
        healthBar.UpdateHealthBar(actor.Life, maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        if (!actor.IsDead) {
            actor.Life -= damageAmount;
            UpdateHealthBar();
            StartCoroutine(TakeDamageEffect());
            if (actor.IsDead) {
                PlayerDie();
            }
        }
    }

    void PlayerDie() {
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        GetComponent<UxrSmoothLocomotion>().enabled = false;
    }

    private IEnumerator ShowGameOverUI() {
        yield return new WaitForSeconds(1f);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("enemy_hand"))
    //     {
    //         
    //         Debug.Log("Debug attack Damage");
    //         float damageAmount = other.gameObject.GetComponent<crypto_enemy_hand>().damage * 0.5f;
    //         TakeDamage(damageAmount);
    //
    //         Vector3 knockbackDirection = (transform.position - other.transform.position).normalized;
    //         rb.AddForce(knockbackDirection * 1f, ForceMode.Impulse);
    //         // actor.ReceiveDamage(other.gameObject.GetComponent<crypto_enemy_hand>().damage);
    //     }
    // }
}

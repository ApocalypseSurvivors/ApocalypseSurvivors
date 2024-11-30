using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UltimateXR.Mechanics.Weapons;
using UltimateXR.Locomotion;

public class Player : MonoBehaviour
{
    private UxrActor actor;
    private Rigidbody rb;
    public Volume vol;
    private Vignette vignette;
    public TextMeshProUGUI deathText;
    [SerializeField] float maxHealth = 100;
    private PlayerHealthBar healthBar;
    [SerializeField] private AudioClip _takeDamageAudioClip;
    [SerializeField] private AudioClip _dieAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<UxrActor>();
        actor.Life = maxHealth;

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        healthBar = GetComponentInChildren<PlayerHealthBar>();
        // vol = GetComponentInChildren<PostProcessVolume>();

        if (!vol) {
            Debug.Log("Debug null vol");
        }
        VolumeProfile volumeProfile = vol.profile;
        if (!volumeProfile) {
            Debug.Log("Debug null profile");
        }
        vol.profile.TryGet<Vignette>(out vignette);
        if (!vignette) {
            Debug.Log("Debug null vignette");
        } else {
            vignette.active = false;
        }
        
        if (healthBar != null) {  
            UpdateHealthBar();
        } else {
            Debug.Log("Debug null healthBar 1");
        }
        Debug.Log("Debug player init success");
 
    }

    private bool dead() {
        return actor.Life <= 0;
    }

    private IEnumerator TakeDamageEffect()
    {
        float intensity = 0.7f;

        // Enable the vignette and set initial intensity
        vignette.active = true;
        vignette.intensity.Override(intensity);

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
        vignette.active = false;

        // End the coroutine
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        if (actor.Life > 10) {
            // TakeDamage(time / 5);
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

    public void TakeDamage(float damageAmount, Transform attacker = null)
    {
        if (!dead()) {
            actor.Life -= damageAmount;
            UpdateHealthBar();
            AudioSource.PlayClipAtPoint(_takeDamageAudioClip, transform.position);
            StopCoroutine("TakeDamageEffect"); 
            StartCoroutine(TakeDamageEffect());
            if (dead()) {
                PlayerDie();
            }
        }
        if (attacker) {
            knockBack(attacker);
        }
    }

    void PlayerDie() {
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        UxrSmoothLocomotion motion = GetComponent<UxrSmoothLocomotion>(); 
        deathText.gameObject.SetActive(true);
        if (!motion) {
            Debug.Log("Debug null motion");
        } else {
            motion.enabled = false;
        }
        AudioSource.PlayClipAtPoint(_dieAudioClip, transform.position);
    }

    private IEnumerator ShowGameOverUI() {
        yield return new WaitForSeconds(1f);
    }

    private void knockBack(Transform attacker) {
            Vector3 knockbackDirection = (transform.position - attacker.position).normalized;
            rb.AddForce(knockbackDirection * 1f, ForceMode.Impulse);
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

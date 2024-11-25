using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Mechanics.Weapons;

public class Player : MonoBehaviour
{
    private UxrActor actor;
    private Rigidbody rb;
    [SerializeField] float maxHealth = 100;
    [SerializeField] PlayerHealthBar healthBar;
    public float test_hp;


    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<UxrActor>();
        actor.Life = maxHealth;

        rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<PlayerHealthBar>();
        
        if (healthBar != null) {  
            UpdateHealthBar();
        } else {
            Debug.Log("Debug null healthBar 1");
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        test_hp = actor.Life;
        float time = Time.deltaTime;
        if (actor.Life > 10) {
            //actor.ReceiveDamage(time);
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
        actor.Life -= damageAmount;
        UpdateHealthBar();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy_hand"))
        {
            
            // Debug.Log("Debug Damage");
            float damageAmount = other.gameObject.GetComponent<crypto_enemy_hand>().damage * 0.5f;
            TakeDamage(damageAmount);

            Vector3 knockbackDirection = (transform.position - other.transform.position).normalized;
            rb.AddForce(knockbackDirection * 1f, ForceMode.Impulse);
            // actor.ReceiveDamage(other.gameObject.GetComponent<crypto_enemy_hand>().damage);
        }
    }
}

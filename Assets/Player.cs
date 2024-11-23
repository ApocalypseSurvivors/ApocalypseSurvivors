using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Mechanics.Weapons;

public class Player : MonoBehaviour
{
    private UxrActor actor;
    [SerializeField] float maxHealth = 100;
    [SerializeField] PlayerHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<UxrActor>();
        actor.Life = maxHealth;

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

    public void TakeDamage(int damageAmount)
    {
        actor.Life -= damageAmount;
        UpdateHealthBar();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy_hand"))
        {
            
            // Debug.Log("Debug Damage");
            TakeDamage(other.gameObject.GetComponent<crypto_enemy_hand>().damage);
            // actor.ReceiveDamage(other.gameObject.GetComponent<crypto_enemy_hand>().damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Mechanics.Weapons;

public class Player : MonoBehaviour
{
    private UxrActor actor;
    [SerializeField] float maxHealth = 100;
    [SerializeField] PlayerHealthBar healthBar;
    public float test_hp;

    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<UxrActor>();
        actor.Life = maxHealth;

        healthBar = GetComponentInChildren<PlayerHealthBar>();
        
        if (healthBar != null) { healthBar.UpdateHealthBar(actor.Life, maxHealth); 
        } else {
            Debug.Log("Debug null healthBar");
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        if (actor.Life > 10) {
            actor.ReceiveDamage(time);
        }
        if (healthBar != null) { healthBar.UpdateHealthBar(actor.Life, maxHealth); 
        } else {
            Debug.Log("Debug null healthBar");
        }
        test_hp = actor.Life;
    }

    public void TakeDamage(int damageAmount)
    {
        actor.Life -= damageAmount;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy_hand"))
        {
            TakeDamage(other.gameObject.GetComponent<crypto_enemy_hand>().damage);
        }
    }
}

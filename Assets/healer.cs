using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Mechanics.Weapons;

public class healer : MonoBehaviour
{
    private UxrActor actor;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        
        actor = GetComponent<UxrActor>();
        actor.DamageReceiving += HandleDamage;

        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
    }

    void HandleDamage(object sender, UxrDamageEventArgs e) {
        e.Cancel();
        float damage = e.Damage; 

        Debug.Log("Debug incoming player");
        if (damage < 0) {
            if (!player) {
                Debug.Log("Debug null player");
            } else {
                player.heal(-damage);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

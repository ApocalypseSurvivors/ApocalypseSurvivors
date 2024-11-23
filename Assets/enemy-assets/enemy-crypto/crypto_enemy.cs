using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crypto_enemy : MonoBehaviour
{
    public crypto_enemy_hand crypto_Enemy_Hand;
    public int crypto_enemy_damage;
    // Start is called before the first frame update
    void Start()
    {
        crypto_Enemy_Hand.damage = crypto_enemy_damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

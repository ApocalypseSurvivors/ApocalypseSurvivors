using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public GameObject[] ammos;
    public GameObject syringePrefab;
    [Range(0f, 1f)]
    public float dropProb = 0.8f;
    [Range(0f, 1f)]
    public float dropAmmoProb = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float getRandom() {
        float randomValue = Random.Range(0f, 1f);
        return randomValue;
    }

    public void drop() {
        if (getRandom() < dropProb) {
            dropItem();
        }
    }

    private void dropItem() {
        if (getRandom() < dropAmmoProb) {
            dropAmmo();
        } else {
            dropSyringe();
        }
    }

    private void dropAmmo() {
        int len = ammos.Length;
        if (len > 0) {
            int randomIndex = Random.Range(0, len);
            var ammo = ammos[randomIndex];
            Vector3 spawnPosition = getSpawnlocation();
             Instantiate(ammo, spawnPosition, Quaternion.identity);
        } 
    }

    private Vector3 getSpawnlocation() {
        float offset = 0.1f;
        Vector3 spawnOffset = new Vector3(Random.Range(-offset, offset), 0f, Random.Range(-offset, offset));
        Vector3 spawnPosition = transform.position + spawnOffset;
        return spawnPosition;
    }

    private void dropSyringe() {

        Vector3 spawnPosition = getSpawnlocation();
        var enemy = Instantiate(syringePrefab, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

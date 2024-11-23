using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timeForDetruction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf(timeForDetruction));
    }
    IEnumerator DestroySelf(float timeForDestruction)
    {
        yield return new WaitForSeconds(timeForDestruction);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

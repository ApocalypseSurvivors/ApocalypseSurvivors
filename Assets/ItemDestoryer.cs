using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Manipulation;

public class ItemDestoryer : MonoBehaviour
{
    public float destoryDis = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = UxrAvatar.LocalAvatar.CameraPosition;       
        float dis = Vector3.Distance(transform.position, cameraPos);
        var grabbale = GetComponent<UxrGrabbableObject>();
        if (dis > destoryDis && !grabbale.CurrentAnchor) {
            Destroy(gameObject);
        }
    }
}

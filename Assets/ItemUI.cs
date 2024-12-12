using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Guides;
using UltimateXR.Manipulation;

public class ItemUI : MonoBehaviour
{
    public float range = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = UxrAvatar.LocalAvatar.CameraPosition;       
        Vector3 viewDirection = UxrAvatar.LocalAvatar.CameraForward;
        float dis = Vector3.Distance(transform.position, cameraPos);
        float dot = Vector3.Dot(viewDirection, transform.position - cameraPos);
        var grabbale = GetComponent<UxrGrabbableObject>();
        if (dis < range && 
                // dot > 0 && 
                !grabbale.IsBeingGrabbed) {
            if (dot <= 0) {
                UxrCompass.Instance.SetTarget(transform, UxrCompassDisplayMode.Look);
            } else {
                UxrCompass.Instance.SetTarget(transform, UxrCompassDisplayMode.Grab);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
       slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("No Slider component found on this GameObject or its children.");
        } 
    }

    public void UpdateHealthBar(float currentValue, float maxValue) {
        slider.value = currentValue / maxValue;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       transform.LookAt(Camera.main.transform.position);
    }
}

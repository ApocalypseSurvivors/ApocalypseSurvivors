using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTimer : MonoBehaviour
{
    private TextMeshProUGUI timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = FormatTime(Time.timeSinceLevelLoad); 
    }

    // Method to format time in seconds to "HH:MM:SS"
    public string FormatTime(float totalSeconds)
    {
        // Calculate hours, minutes, and seconds
        int hours = Mathf.FloorToInt(totalSeconds / 3600);
        int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);
        // Format as "HH:MM:SS"
        string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        return formattedTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  

    private float startTime;
    private float bestTime = 0f;
    private bool isRunning = true;

    void Start()
    {
        startTime = Time.time;
        UpdateTimerText(0f, bestTime); 
    }

    void Update()
    {
        if (isRunning)
        {
            float t = Time.time - startTime;
            UpdateTimerText(t, bestTime);  
        }
    }

    private void UpdateTimerText(float currentTime, float bestTime)
    {
        string currentMinutes = ((int)currentTime / 60).ToString();
        string currentSeconds = (currentTime % 60).ToString("f2");

        string bestMinutes = ((int)bestTime / 60).ToString();
        string bestSeconds = (bestTime % 60).ToString("f2");

        
        timerText.text = "Time: " + currentMinutes + ":" + currentSeconds + "\n" +
                         "Best Time: " + bestMinutes + ":" + bestSeconds;
    }

    public void ResetTimer()
    {
        float currentTime = Time.time - startTime;
        if (currentTime > bestTime)
        {
            bestTime = currentTime;
            UpdateTimerText(currentTime, bestTime);  
        }

        startTime = Time.time;  
    }

    public void StopTimer()
    {
        isRunning = false;
        float currentTime = Time.time - startTime;
        if (currentTime > bestTime)
        {
            bestTime = currentTime;
            UpdateTimerText(currentTime, bestTime); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool timerRunning = true;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (!timerRunning)
            return;

        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;
    }

    // Function to pause the timer
    public void PauseTimer()
    {
        timerRunning = false;
    }

    // Function to resume the timer
    public void ResumeTimer()
    {
        timerRunning = true;
    }
}
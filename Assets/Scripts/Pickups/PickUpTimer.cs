using System;
using UnityEngine;

public class PickUpTimer : MonoBehaviour
{
    public static float time;
    public static float currentTime
    {
        get;
        private set;
    }
    public static bool isUsed;
    public static bool isRunning;
    public static event Action TimerEnded; 


    private void Update()
    {
        if (isRunning)
        {
            if (currentTime < time)
            {
                currentTime += Time.unscaledDeltaTime;
            }
            else
            {
                currentTime = time;
                StopTimer();
                TimerEnded();
            }
        }
    }
    
    public static void SetTimer(float t)
    {
        currentTime = 0f;
        isRunning = true;
        isUsed = true;
        time = t;
    }

    public static void ResumeTimer()
    {
        isRunning = true;
    }

    public static void PauseTimer()
    {
        isRunning = false;
    }

    public static void StopTimer()
    {
        currentTime = 0;
        isRunning = false;
        isUsed = false;
    }

    public static void ToggleTimer()
    {
        isRunning = !isRunning;
    }
}

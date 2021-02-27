using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// использовать его в TimePickup
public class TimeManager : MonoBehaviour
{
    public bool slowed = false;

    void Start ()
    {
        Time.timeScale = 1;
        slowed = false;
	}
	
    public void ToggleTimescale()
    {
        if (!slowed)
        {
            Time.timeScale = 0.2f;
            slowed = true;
        }
        else
        {
            Time.timeScale = 1;
            slowed = false;
        }
    }
    public void ToggleTimescale(bool state)
    {
        if (slowed != state)
            ToggleTimescale();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public TMP_Text timerText;
    private float time;
    private bool isRunning = true;

    TimeSpan timespan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            time += Time.deltaTime;
        }

        timespan = TimeSpan.FromSeconds(time);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
    }
}

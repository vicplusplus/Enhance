using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMProUI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float timeRemaining = 10;
    public bool timerISRunning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // The timer will start automatically
        timerISRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerISRunning = false;
            }
        }
    }
    
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

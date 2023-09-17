using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private ScoreManager scoreManager;
    
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI finalScoreText;
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // The timer will start automatically
        timerIsRunning = true;
        // Getting score reference
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
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
                timerIsRunning = false;
            }
        }
        else
        {
            finalScoreText.gameObject.SetActive(true);
            finalScoreText.text = "Your final score is " + scoreManager.score + "!";
        }
    }
    
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

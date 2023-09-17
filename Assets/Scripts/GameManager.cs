using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public State state;
    public float initialTimer;
    private float timeRemaining;
    private int score;
    public TMP_Text scoreDisplay;
    public TMP_Text timeDisplay;
    public TMP_Text finalScoreDisplay;
    public TMP_Text highScoreDisplay;
    public ItemSpawner itemSpawner;
    public ItemPile itemPile;
    public PullCord pullCord;
    public Transform mainMenu;
    public Transform hud;
    public Transform endMenu;

    public int Score
    {
        get => score;
        set
        {
            if (state != State.Playing) return;
            score = value;
            scoreDisplay.text = $"Score: {value}";
        }
    }

    public float TimeRemaining
    {
        get => timeRemaining;
        set
        {
            timeRemaining = value;
            DisplayTime(value);
        }
    }

    public enum State
    {
        MainMenu,
        Playing,
        Completed
    }

    private void Start()
    {
        LoadMainMenu();
    }

    private void Update()
    {
        switch(state)
        {
            case State.Playing:
                TimeRemaining -= Time.deltaTime;
                if (TimeRemaining <= 0)
                {
                    EndGame();
                }
                break;
        }
    }

    public void LoadMainMenu()
    {
        state = State.MainMenu;
        mainMenu.gameObject.SetActive(true);
        hud.gameObject.SetActive(false);
        endMenu.gameObject.SetActive(false);
        pullCord.enabled = false;
        int highScore = PlayerPrefs.GetInt("High Score", 0);
        highScoreDisplay.text = highScore > 0 ? $"High Score: {highScore}" : "";
    }

    public void StartGame()
    {
        state = State.Playing;
        mainMenu.gameObject.SetActive(false);
        hud.gameObject.SetActive(true);
        endMenu.gameObject.SetActive(false);
        Score = 0;
        TimeRemaining = initialTimer;
        pullCord.enabled = true;
        Destroy(itemSpawner.currentObject);
        foreach (Transform t in itemPile.transform)
        {
            Destroy(t.gameObject);
        }
    }

    public void EndGame()
    {
        state = State.Completed;
        mainMenu.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);
        endMenu.gameObject.SetActive(true);
        finalScoreDisplay.text = $"Your final score is: {score}";
        if (PlayerPrefs.GetInt("High Score", 0) < score)
        {
            finalScoreDisplay.text += "\n High Score!";
            PlayerPrefs.SetInt("High Score", score);
        }
        if(itemSpawner.currentObject) itemSpawner.currentObject.GetComponent<Item>().enabled = false;
        pullCord.enabled = false;
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

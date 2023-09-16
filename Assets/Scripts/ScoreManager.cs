using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreDisplay.text = $"Score: {value}";
        }
    }
    public TMP_Text scoreDisplay;

    private int score;

    private void Awake()
    {
        scoreDisplay = GetComponentInChildren<TMP_Text>();
    }
}

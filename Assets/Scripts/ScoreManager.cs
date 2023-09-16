using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Uses a public property with a getter/setter in place of a function
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

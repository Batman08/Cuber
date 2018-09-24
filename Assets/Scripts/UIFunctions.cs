﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour
{
    public Text _scoreText;
    public int _score;
    public bool _gameStarted;

    void Start()
    {
        _gameStarted = true;

        InvokeRepeating("UpdateScore", 1.0f, 1.0f);
    }

    private void Update()
    {
        SaveScore();
    }

    private void SaveScore()
    {
        string highScoreKey = "HighScore";
        bool ScoreIsGreaterThanHighscore = (PlayerPrefs.GetInt(highScoreKey) < _score);
        if (ScoreIsGreaterThanHighscore)
        {
            PlayerPrefs.SetInt(highScoreKey, _score);
            PlayerPrefs.Save();
        }
    }

    private void UpdateScore()
    {
        if (_gameStarted == true)
        {
            _score++;
            _scoreText.text = "" + _score;
        }
    }
}

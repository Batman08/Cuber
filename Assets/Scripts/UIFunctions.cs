using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIFunctions : MonoBehaviour
{
    public TextMeshProUGUI _scoreText;
    public TextMeshProUGUI _scoreText2;
    public int _score;
    public bool _gameStarted;

    private PauseMenu _pauseMenu;

    void Start()
    {
        _gameStarted = true;

        _pauseMenu = FindObjectOfType<PauseMenu>();

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
        if (_gameStarted == true && !_pauseMenu.HasPausedGame)
        {
            _score++;
            _scoreText.text = "" + _score;
            _scoreText2.text = "" + _score;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Text ChangeControlText;
    public Text HighScoreText;

    public int control;

    private string ControlsKey = "Controls";
    private JsonData _jsonData;

    void Start()
    {
        Time.timeScale = 1;
        control = PlayerPrefs.GetInt(ControlsKey);
        _jsonData = FindObjectOfType<JsonData>();
    }

    void Update()
    {
        ChangeControlButtonText();
        _jsonData.ReadData();
        LoadHighScore();
    }

    private void ChangeControlButtonText()
    {
        bool TouchControl = control == 0;
        if (TouchControl)
            ChangeControlText.text = "Touch";
        else
            ChangeControlText.text = "Tilt";
    }

    private void LoadHighScore()
    {
        string highScoreKey = "HighScore";
        HighScoreText.text = "HighScore: " + PlayerPrefs.GetInt(highScoreKey).ToString();
    }

    //public void Play()
    //{
    //    SceneManager.LoadScene("GameScene");
    //}

    public void ChangeControlsOption()
    {
        control = (control == 0) ? 1 : 0;
        PlayerPrefs.SetInt(ControlsKey, control);
        Debug.Log(PlayerPrefs.GetInt(ControlsKey));
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Exiting Game!!!");
#endif
        Application.Quit();
    }
}

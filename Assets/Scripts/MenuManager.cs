using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Text Mesh Pro Text")]
    public TextMeshProUGUI ChangeControlText;
    public TextMeshProUGUI ChangeControlText2;
    [Space]
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI HighScoreText2;

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
        {
            ChangeControlText.text = "Touch";
            ChangeControlText2.text = "Touch";
        }

        else
        {
            ChangeControlText.text = "Tilt";
            ChangeControlText2.text = "Tilt";
        }
    }

    private void LoadHighScore()
    {
        string highScoreKey = "HighScore";
        HighScoreText.text = "HighScore: " + PlayerPrefs.GetInt(highScoreKey).ToString();
        HighScoreText2.text = "HighScore: " + PlayerPrefs.GetInt(highScoreKey).ToString();
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

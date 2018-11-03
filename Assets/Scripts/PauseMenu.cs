using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool HasPausedGame = false;
    public GameObject PauseMenuUI;
    public GameObject PauseButton;

    void Awake()
    {
        Time.timeScale = 1;
        PauseMenuUI.SetActive(value: false);
    }

    public void PauseGame()
    {
        HasPausedGame = true;
        PauseMenuUI.SetActive(value: true);
        Time.timeScale = 0;
        PauseButton.SetActive(value: false);
    }

    public void Resume()
    {
        HasPausedGame = false;
        PauseMenuUI.SetActive(value: false);
        PauseButton.SetActive(value: true);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}

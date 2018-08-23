using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Text ChangeControlText;

    public int control;

    private string ControlsKey = "Controls";

    void Start()
    {
        control = PlayerPrefs.GetInt(ControlsKey);
    }

    void Update()
    {
        ChangeControlButtonText();
    }

    private void ChangeControlButtonText()
    {
        bool TouchControl = control == 0;
        if (TouchControl)
            ChangeControlText.text = "Touch";
        else
            ChangeControlText.text = "Tilt";
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

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

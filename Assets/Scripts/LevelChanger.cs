using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator Animator;

    private string LevelToLoadString = "";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GoToLevel()
    {
        FadeToLevel("GameScene");
    }

    public void FadeToLevel(string levelName)
    {
        LevelToLoadString = levelName;
        Animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(LevelToLoadString);
        Animator.SetTrigger("FadeIn");
    }
}

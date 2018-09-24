using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator Animator;

    private string LevelToLoadString = "";

    public int Dice1, Dice2;
    private AdManager _adManager;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        _adManager = FindObjectOfType<AdManager>();
        Dice1 = UnityEngine.Random.Range(0, 4);
        Dice2 = UnityEngine.Random.Range(0, 4);
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
        bool BothDicesEqualEachOther = (Dice1 == Dice2);
        if (BothDicesEqualEachOther)
        {
            _adManager.ShowInterstitialAd();
        }
        SceneManager.LoadScene(LevelToLoadString);
        Animator.SetTrigger("FadeIn");
    }
}

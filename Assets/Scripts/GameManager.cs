using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Variables")]
    public GameObject TimerTextGameObject;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI TimerText2;
    [Space]
    public GameObject ShowAdButton;
    public ParticleSystem ParticleSystem;

    public GameObject DuringGameText;
    public GameObject EndGamePanel;

    public bool ShouldGiveAnotherChance;
    public bool IsGameOver = false;
    public bool CanShowTimer = false;
    public float TimerTime = 3f;


    private LivesManager _livesManager;
    private AdManager _adManager;
    private PauseMenu _pauseMenu;
    private LoadingBar _loadingBar;

    void Start()
    {
        _livesManager = FindObjectOfType<LivesManager>();
        _adManager = FindObjectOfType<AdManager>();
        _pauseMenu = FindObjectOfType<PauseMenu>();
        _loadingBar = FindObjectOfType<LoadingBar>();
        Physics.IgnoreLayerCollision(10, 11, false);
        ShouldGiveAnotherChance = true;

        //ParticleSystem = FindObjectOfType<ParticleSystem>();
        //ParticleSystem.Stop();

        ShowAdButton.SetActive(value: false);
        TimerTextGameObject.SetActive(value: false);
    }

    void Update()
    {
        CheckIfGameIsOver();
        ShowTimer();
    }

    private void CheckIfGameIsOver()
    {
        bool gameIsOver = (_livesManager.Lives <= 0);
        if (gameIsOver)
        {
            IsGameOver = true;
            bool buttonIsStillLoading = _loadingBar._currentAmount > 0 && ShouldGiveAnotherChance;
            if (buttonIsStillLoading)
            {
                ShowAdButton.SetActive(value: true);
            }

            else
            {
                DuringGameText.SetActive(value: false);
                EndGamePanel.SetActive(value: true);
            }
            //Time.timeScale = 0;
            _pauseMenu.HasPausedGame = true;

        }
    }

    void ShowTimer()
    {
        if (CanShowTimer)
        {
            TimerTime -= Time.deltaTime;
            TimerText.text = "" + TimerTime.ToString("F0");
            TimerText2.text = "" + TimerTime.ToString("F0");
            if (TimerTime <= 0)
            {
                TimerTextGameObject.SetActive(value: false);
                _pauseMenu.HasPausedGame = false;
                StartCoroutine(PlayerInvincibility());
                CanShowTimer = false;
                TimerTime = 3f;
            }
        }
    }

    public void ShowAd()
    {
        _adManager.ShowVideoAd();
        _livesManager.Lives = 1;
        ShowAdButton.SetActive(value: false);
        CanShowTimer = true;
        _pauseMenu.HasPausedGame = true;
        Time.timeScale = 1;
        ShouldGiveAnotherChance = false;
        TimerTextGameObject.SetActive(value: true);
    }

    private IEnumerator PlayerInvincibility()
    {
        Physics.IgnoreLayerCollision(10, 11, true);
        yield return new WaitForSeconds(8f);
        Physics.IgnoreLayerCollision(10, 11, false);
    }

    #region Don't know if needed
    public void RightButton()
    {
        //ParticleSystem.gameObject.SetActive(value: true);
        //ParticleSystem.Play();
        //ParticleSystem.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    public void LeftButton()
    {
        //ParticleSystem.gameObject.SetActive(value: true);
        //ParticleSystem.Play();
        //ParticleSystem.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void StopParticleSystem()
    {
        //ParticleSystem.Stop();
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ShowAdButton;
    public ParticleSystem ParticleSystem;
    public bool IsGameOver = false;
    public bool IsParticleSystemActive = false;

    private LivesManager _livesManager;
    private AdManager _adManager;

    void Start()
    {
        _livesManager = GetComponent<LivesManager>();
        _adManager = FindObjectOfType<AdManager>();

        ParticleSystem = FindObjectOfType<ParticleSystem>();
        ShowAdButton.SetActive(value: false);
    }

    void Update()
    {
        CheckIfGameIsOver();
    }

    private void CheckIfGameIsOver()
    {
        bool gameIsOver = (_livesManager.Lives <= 0);
        if (gameIsOver)
        {
            IsGameOver = true;
            ShowAdButton.SetActive(value: true);
            Time.timeScale = 0;
        }
    }

    public void ShowAd()
    {
        _adManager.ShowVideoAd();
        _livesManager.Lives = 1;
        ShowAdButton.SetActive(value: false);
        Time.timeScale = 1;
        StartCoroutine(PlayerInvincibility());
    }

    private IEnumerator PlayerInvincibility()
    {
        Physics.IgnoreLayerCollision(10, 11, true);
        yield return new WaitForSeconds(8f);
        Physics.IgnoreLayerCollision(10, 11, false);
    }

    public void RightButton()
    {
        IsParticleSystemActive = true;
        ParticleSystem.gameObject.SetActive(value: true);
        ParticleSystem.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    public void LeftButton()
    {
        IsParticleSystemActive = true;
        ParticleSystem.gameObject.SetActive(value: true);
        ParticleSystem.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void DisableSystem()
    {
        IsParticleSystemActive = false;
    }
}

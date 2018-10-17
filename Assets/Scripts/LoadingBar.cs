using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LoadingBar : MonoBehaviour
{
    public Transform LoadingBarTransform;
    public GameObject EndGamePanel;
    public GameObject DuringGameText;
    public TextMeshProUGUI FinalScoreText;
    public TextMeshProUGUI FinalScoreText2;

    [HideInInspector] [SerializeField] public float _currentAmount = 100;
    [SerializeField] private float _speed;
    private UIFunctions _uiFunctions;

    private void Awake()
    {
        _currentAmount = 100f;
        _uiFunctions = FindObjectOfType<UIFunctions>();
        EndGamePanel.SetActive(value: false);
    }

    void Update()
    {
        StartProgressBar();
    }

    private void StartProgressBar()
    {
        if (_currentAmount > 0)
        {
            _currentAmount -= _speed * Time.deltaTime;
        }

        else
        {
            Debug.Log("Game Over");
            DuringGameText.SetActive(value: false);
            EndGamePanel.SetActive(value: true);
            LoadScore();
            gameObject.SetActive(value: false);
        }

        LoadingBarTransform.GetComponent<Image>().fillAmount = _currentAmount / 100;
    }

    private void LoadScore()
    {
        FinalScoreText.text = "" + _uiFunctions._score;
        FinalScoreText2.text = "" + _uiFunctions._score;
    }
}//sofetness ----- 0.572

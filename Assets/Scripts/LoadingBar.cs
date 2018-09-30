using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    public Transform LoadingBarTransform;

    [HideInInspector] [SerializeField] public float _currentAmount = 100;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _currentAmount = 100f;
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
            gameObject.SetActive(value: false);
        }

        LoadingBarTransform.GetComponent<Image>().fillAmount = _currentAmount / 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public GameObject PlayerObj;
    public Text LivesText;
    public int Lives;

    private int _startLives = 3;

    void Start()
    {
        Lives = _startLives;

        //Instantiate(PlayerObj);
    }

    void Update()
    {
        UpdateText();
        CheckForLives();
    }

    private void UpdateText()
    {
        LivesText.text = "Lives: " + Lives;

        bool livesAreLessThanZero = (Lives <= 0);
        if (livesAreLessThanZero)
        {
            Lives = 0;
        }
    }

    private void CheckForLives()
    {
        bool hasNoLivesLeft = (Lives <= 0);

        if (hasNoLivesLeft)
        {
            Destroy(PlayerObj);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

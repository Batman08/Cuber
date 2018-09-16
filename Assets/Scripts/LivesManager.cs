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
    public bool HasInfinitLives = false;
    public Material PlayerMat;
    public Color Color;
    public Color Color2;
    public Color Color3;

    private int _startLives = 3;


    void Start()
    {
        Lives = _startLives;
        PlayerMat.color = Color;
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

        if (hasNoLivesLeft && !HasInfinitLives)
        {
            //Destroy(PlayerObj);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Time.timeScale = 1;
        }

        if (Lives == 3)
        {
            PlayerMat.color = Color;
        }

        if (Lives == 2)
        {

            PlayerMat.color = Color2;
        }

        if (Lives == 1)
        {
            PlayerMat.color = Color3;
        }
    }
}

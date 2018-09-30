using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public GameObject PlayerObj;
    [Space]
    [Header("Lives Text")]
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI LivesText2;
    [Space]
    public int Lives;
    public bool HasInfinitLives = false;
    public Material PlayerMat;
    [Space]
    [Header("Player Colours")]
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
        LivesText.text = "" + Lives;
        LivesText2.text = "" + Lives;

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

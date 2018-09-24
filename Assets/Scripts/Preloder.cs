using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Preloder : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData : MonoBehaviour
{
    private string _fileName = "UserData.json";
    private string _path;

    private GameData gameData = new GameData();

    void Start()
    {
        _path = Application.persistentDataPath + "/" + _fileName;
        Debug.Log(_path);

        //PlayerPrefs.DeleteKey("HighScore");
    }

    private void UpdatePLayerPrefs()
    {
        string ScoreKey = "HighScore";
        bool HighScoreIsGreaterThanScore = (gameData.HighScore >= PlayerPrefs.GetInt(ScoreKey));
        if (HighScoreIsGreaterThanScore)
        {
            PlayerPrefs.SetInt(ScoreKey, gameData.HighScore);
            Debug.Log(gameData.HighScore);
            Debug.Log(PlayerPrefs.GetInt("HighScore"));
        }

        else
        {
            gameData.HighScore = PlayerPrefs.GetInt(ScoreKey);
            Debug.Log(gameData.HighScore);
            Debug.Log(PlayerPrefs.GetInt("HighScore"));
        }
    }

    void Update()
    {
        SaveData();
        UpdatePLayerPrefs();
    }

    private void SaveData()
    {
        JsonWrapper wrapper = new JsonWrapper();
        wrapper.GameData = gameData;

        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(_path, contents);
    }

    public void ReadData()
    {
        try
        {
            bool fileDoesExist = (System.IO.File.Exists(_path));
            if (fileDoesExist)
            {
                string contents = System.IO.File.ReadAllText(_path);
                JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(contents);
                gameData = wrapper.GameData;
                //Debug.Log(gameData.HighScore);
                //Debug.Log(PlayerPrefs.GetInt("HighScore"));
            }

            else
            {
                Debug.Log("Couldn't read file");
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}

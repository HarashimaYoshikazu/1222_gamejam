using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager
{
    public static ApplicationManager Instance = new ApplicationManager();

    public int Score => _score;

    public int CurrentGameNum => _currentGameNum;

    int _score;
    int _currentGameNum;
    List<string> SceneNameList = new List<string>();

    public void GameNumIncrement()
    {
        _currentGameNum++;
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    public void ResetGame()
    {
        Debug.Log("SceneNameíçà”");
        _score = 0;
        _currentGameNum = 0;
        SceneNameList.Add("JiroGameScene");
        SceneNameList.Add("ResultTest");
        SceneNameList.Add("Fukumen_master");
    }

    public void RandomSceneChange()
    {
        string sceneName = "";
        if(SceneNameList.Count == 0)
        {
            sceneName = "MainResultScene";
        }
        int random = Random.Range(0, SceneNameList.Count);
        sceneName = SceneNameList[random];
        SceneNameList.RemoveAt(random);
        SceneChangeController.LoadScene(sceneName);
    }
}

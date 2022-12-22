using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager
{
    public static ApplicationManager Instance = new ApplicationManager();

    public static int Score => _score;

    static int _score;
    List<string> SceneNameList = new List<string>();

    public void AddScore(int score)
    {
        _score += score;
    }

    public void ResetGame()
    {
        Debug.Log("SceneNameíçà”");
        _score = 0;
        SceneNameList.Add("JiroGameScene");
        SceneNameList.Add("SpriteTest");
        SceneNameList.Add("Fukumen_master");
    }

    public void RandomSceneChange()
    {
        string sceneName = "";
        if(SceneNameList.Count == 0)
        {
            sceneName = "MainResultScene";
        }
        else
        {
            int random = Random.Range(0, SceneNameList.Count);
            sceneName = SceneNameList[random];
            SceneNameList.RemoveAt(random);
        }
        SceneChangeController.LoadScene(sceneName);
    }
}

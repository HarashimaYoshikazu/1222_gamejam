using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManagerAttachMent : MonoBehaviour
{
    public void SceneChange()
    {
        ApplicationManager.Instance.RandomSceneChange();
    }

    public void ResetGame()
    {
        ApplicationManager.Instance.ResetGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager
{
    public static ApplicationManager Instance = new ApplicationManager();

    int _score;

    public int Score => _score;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] Text _text;
    void Start()
    {
        _text.text = $"Score:{ApplicationManager.Score.ToString()}";
    }
}

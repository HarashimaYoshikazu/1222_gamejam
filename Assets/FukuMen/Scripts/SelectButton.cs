using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class SelectButton : MonoBehaviour
{
    [SerializeField]
    CharacterType _characterType;
    Button _button;
    public Action OnClick;

    public void Init()
    {
        _button= GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            OnClick.Invoke();
        });
    }
    private void OnDestroy()
    {
        OnClick = null;
    }

}

public enum CharacterType
{
    Almond,
    Coin,
    Egg
}
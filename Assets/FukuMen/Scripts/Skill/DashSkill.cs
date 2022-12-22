using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : MonoBehaviour, ISkill
{
    [SerializeField]
    private int _dashValue = 2;

    private CharaController _charaController;

    private void Start()
    {
        _charaController = GetComponent<CharaController>();
    }

    private bool isSkill = false;
    public async void UseSkill(ControllType controllType)
    {
        if (isSkill == true) return;
        
        _charaController.SetPosition(_dashValue);
        isSkill = true;
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        isSkill = false;
    }
}

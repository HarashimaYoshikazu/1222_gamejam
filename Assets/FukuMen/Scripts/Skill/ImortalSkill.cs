using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImortalSkill : MonoBehaviour, ISkill
{
    [SerializeField]
    private int _imortalTime = 5;

    private CharaController _charaController;

    private void Start()
    {
        _charaController = GetComponent<CharaController>();
    }

    public async void UseSkill()
    {
        _charaController.IsHit = true;
        await UniTask.Delay(TimeSpan.FromSeconds(_imortalTime));
        _charaController.IsHit = false;
    }
}

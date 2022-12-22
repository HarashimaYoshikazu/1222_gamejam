using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImortalSkill : MonoBehaviour, ISkill
{
    [SerializeField]
    private CharaController _charaController;

    public async void UseSkill()
    {
        _charaController.IsHit = true;
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        _charaController.IsHit = false;
    }
}

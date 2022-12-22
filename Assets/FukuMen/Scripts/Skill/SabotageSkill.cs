using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageSkill :MonoBehaviour, ISkill
{
    private CharaController[] players;
    private CharaController player;

    private void Start()
    {
        player = GetComponent<CharaController>();
        players = FindObjectsOfType<CharaController>();
    }

    private bool isSkill = false;
    public async void UseSkill(ControllType controllType)
    {
        if (isSkill == true) return;
        foreach (CharaController c in players)
        {
            if(c != player)
            {
                c.SetPosition(-2);
            }
        }
        isSkill = true;
        await UniTask.Delay(TimeSpan.FromSeconds(10f));
        isSkill = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : MonoBehaviour, ISkill
{
    [SerializeField]
    private CharaController _charaController;
    [SerializeField]
    private int _dashValue = 2;

    public void UseSkill()
    {
        _charaController.SetPosition(_dashValue);
    }
}

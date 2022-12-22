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

    public void UseSkill()
    {
        _charaController.SetPosition(_dashValue);
    }
}

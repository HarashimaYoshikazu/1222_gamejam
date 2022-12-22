using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharaController : MonoBehaviour
{
    [SerializeField, Range(-5, 5)]
    private int _speedPower = 0;

    [SerializeField]
    private bool _isGrounded = false;

    [SerializeField]
    private bool _isDamage = false;

    [Inject]
    IJump _iJump;

    private void Update()
    {
        if (_iJump.IsJump())
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField]
    private int _speedPower = 0;

    [SerializeField]
    private bool _isGrounded = false;

    [SerializeField]
    private bool _isDamage = false;

    IJump _iJump;

    private void Update()
    {
        if(_iJump.IsJump())
        {

        }
    }
}

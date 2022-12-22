using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField]
    CharaType _charaType;

    [SerializeField, Range(-5, 5)]
    private int _speedPower = 0;

    [SerializeField]
    private float _jumpPower = 0f;

    [SerializeField]
    private float _jumpInterval = 0f;

    [SerializeField]
    private bool _isGrounded = false;

    [SerializeField]
    private bool _isDamage = false;

    IJump _iJump;

    Rigidbody _rb;

    private void Start()
    {
        if (_charaType == CharaType.PLAYER)
        {
            _iJump = GetComponent<PlayerInput>();
        }
        else if (_charaType == CharaType.CPU)
        {
            _iJump= GetComponent<EnemyInput>();
        }

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_iJump.IsJump())
        {
            _rb.AddForce(transform.up * _jumpPower, ForceMode.Impulse);
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Grounded"))
        {
            StartCoroutine(JumpInterval());
        }
        if (other.gameObject.CompareTag("asteroid"))
        {
            _speedPower--;
        }
    }

    IEnumerator JumpInterval()
    {
        yield return new WaitForSeconds(_jumpInterval);
        _isGrounded = false;
    }
}

enum CharaType
{
    PLAYER,
    CPU,
}
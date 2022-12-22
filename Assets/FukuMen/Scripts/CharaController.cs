using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField]
    ControllType _controllType;

    [SerializeField]
    CharacterType _characterType;
    public CharacterType CharacterType => _characterType;

    [SerializeField, Range(0, 10)]
    private int _speedPower = 5;

    [SerializeField]
    private float _jumpPower = 0f;

    [SerializeField]
    private float _jumpInterval = 0f;

    [SerializeField]
    private bool _isGrounded = false;

    [SerializeField]
    private bool _isDamage = false;

    [SerializeField]
    private Transform[] _speedPosition = new Transform[11];

    IJump _iJump;

    Rigidbody _rb;

    private void Start()
    {
        if (_controllType == ControllType.PLAYER)
        {
            _iJump = new PlayerInput();
        }
        else if (_controllType == ControllType.CPU)
        {
            _iJump= new EnemyInput();
        }

        SetPosition(_speedPower);
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_iJump.IsJump(gameObject))
        {
            _rb.AddForce(transform.up * _jumpPower, ForceMode.Impulse);
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(JumpInterval());
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            _speedPower--;
            SetPosition(_speedPower);
        }
    }

    public void SetPosition(int index)
    {
        this.transform.position = _speedPosition[index].position;
    }

    IEnumerator JumpInterval()
    {
        yield return new WaitForSeconds(_jumpInterval);
        _isGrounded = false;
    }
}

enum ControllType
{
    PLAYER,
    CPU,
}
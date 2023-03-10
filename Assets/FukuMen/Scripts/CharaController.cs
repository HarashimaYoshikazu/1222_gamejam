using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField]
    public ControllType _controllType;

    public ControllType ControllType { get { return _controllType; } set { _controllType= value; } }
    [SerializeField, Range(-5, 5)]
    private int _currentPosition = 0;
    public int CurrentPosition { get { return _currentPosition; } }

    [SerializeField]
    private float _jumpPower = 0f;

    [SerializeField]
    private float _jumpInterval = 0f;

    [SerializeField]
    private bool _isGrounded = false;

    [SerializeField]
    private bool _isDamage = false;

    [SerializeField]
    float _moveInterval = 2f;

    IInput _iInput;
    ISkill _iSkill;

    Rigidbody _rb;
    Animator _anim;
    AudioPlayer _audioplayer;

    [SerializeField]
    GameObject _text;
    private void Start()
    {
        if (_controllType == ControllType.PLAYER)
        {
            _iInput = new PlayerInput();
            _text.SetActive(true);
        }
        else if (_controllType == ControllType.CPU)
        {
            _iInput = new EnemyInput();
            _text.SetActive(false);
        }
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _audioplayer = GetComponent<AudioPlayer>();
        _iSkill = GetComponent<ISkill>();
    }

    private void Update()
    {
        if (_iInput.IsJump(gameObject))
        {
            if(!_isGrounded)
            {
                _rb.AddForce(transform.up * _jumpPower, ForceMode.Impulse);
                _audioplayer.AudioPlay("Jump");
                _isGrounded = true;
                Debug.Log("Jump");
            }
        }

        if(_iInput.IsSkill())
        {
            _iSkill.UseSkill(_controllType);
        }
    }

    public bool IsHit = false;
    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(JumpInterval());
        }
        if (other.gameObject.CompareTag("Obstacle") && IsHit == false)
        {
            _anim?.Play("Damage");
            _audioplayer.AudioPlay("Damage");
            SetPosition(-1);

            //3?b???????????????t???O?????????????G??????
            IsHit = true;
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            IsHit = false;
        }
    }

    public void SetPosition(int value)
    {
        _currentPosition = Mathf.Clamp(_currentPosition + value, -5, 5);
        Vector3 newPosotion = this.transform.position;
        newPosotion.z += Mathf.Clamp(_currentPosition * _moveInterval, -5 * _moveInterval, 5 * _moveInterval);
        this.transform.position = newPosotion;
    }

    IEnumerator JumpInterval()
    {
        yield return new WaitForSeconds(_jumpInterval);
        _isGrounded = false;
    }

    public void Goal()
    {
        _anim?.Play("Goal");
        _audioplayer.AudioPlay("Goal");
    }
}

public enum ControllType
{
    PLAYER,
    CPU,
}
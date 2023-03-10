using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Bonnou
{
    public class CameraAnimation : MonoBehaviour
    {
        [SerializeField] Transform _begin, _end;
        [SerializeField] float _moveSpeed = 10f;
        [SerializeField] float _delayTime = 3f;
        [SerializeField] Ease _moveEase = Ease.OutQuart;
        [Space(10)]
        [SerializeField] Text _startText, _stopText;
        [Space(10)]
        [SerializeField] Image _fadePanel, _backImage;
        [SerializeField] Ease _fadeEase = Ease.Linear;
        [SerializeField] float _fadeSpeed = 0.15f;
        [Space(10)]
        [SerializeField] float _shakeDuration = 0.2f;
        [SerializeField] float _strength = 0.2f;
        [Space(10)]
        [SerializeField] AudioPlayer _player;
        [SerializeField] Text _text;

        Transform _camTransform;
        Tween _anim;
        Fade _fade;

        bool _isStart;

        bool _isEnd;

        private void Awake()
        {
            _camTransform = Camera.main.transform;

            if (!_startText || !_stopText)
            {
                Debug.Log($"どちらかのテキストがアセットされていません");
            }
            else
            {
                _stopText.enabled = false;
                _startText.enabled = true;
            }

            if (_fadePanel)
            {
                _fade = new Fade(_fadePanel);
            }
            else
            {
                Debug.Log($"フェードパネルがアセットされていません");
            }
        }

        private void Start()
        {

        }
        private void Update()
        {
            if (!_isStart)
            {
                if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
                {
                    _isStart = true;
                    OnPlay();
                }

                return;
            }

            if (!_isEnd)
            {
                if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
                    OnStop();
            }
            else
            {
                if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
                    ApplicationManager.Instance.RandomSceneChange();
            }
        }

        public void OnPlay()
        {
            _startText.enabled = false;
            _stopText.enabled = true;
            _player.PlaySound(3);

            _anim = _camTransform.DOMove(_end.position, _moveSpeed)
                .SetEase(_moveEase)
                .SetDelay(_delayTime)
                .OnComplete(() =>
                {

                    _fade.OnFadeInOut(_fadeSpeed, _fadeEase, () => OnAction());

                    Debug.Log($"終了地点に到着");
                });

            _anim.Play();
        }
        void OnAction()
        {
            if (_backImage)
            {
                _backImage.enabled = true;
            }
            else
            {
                Debug.Log($"遷移後のイメージがありません");
            }
        }
        public void OnStop()
        {
            if (_anim != null)
            {
                _anim.Pause();
            }
            else
            {
                Debug.Log($"アニメーションが登録されていません");
            }

            _camTransform.position = _begin.position;
            _camTransform.DOShakePosition(_shakeDuration, _strength);
            _fade.Kill();
            _backImage.enabled = false;
            _player.PlaySound(2);
            _isEnd = true;
            _text.enabled = true;
            _stopText.enabled = false;
        }
    }
}

namespace Bonnou
{

    public class Fade
    {
        Image _image;
        Tween _tween;

        public Fade(Image panel)
        {
            _image = panel;
        }

        public void Kill()
        {
            if (_tween != null)
            {
                _tween.Kill();
            }
        }

        public void OnFadeIn(float duration, Ease ease, Action action)
        {
            _tween = _image.DOFade(1, duration)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    action();
                });

            _tween.Play();
        }
        public void OnFadeInOut(float duration, Ease ease, Action action)
        {
            _tween = _image.DOFade(1, duration)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    action();
                    OnFadeOut(duration, ease);
                });

            _tween.Play();
        }
        public void OnFadeOut(float duration, Ease ease)
        {
            _tween = _image.DOFade(0, duration)
                .SetEase(ease);

            _tween.Play();
        }
    }
}
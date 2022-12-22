using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Bonnou
{
    public class TimeCounter : MonoBehaviour
    {

        [SerializeField] float _startTime;
        private const int LimitTime = 60;
        [SerializeField] float _passTime = 0.2f;

        [SerializeField] float _firstActionTime;

        float _score;
        [SerializeField] float _baseScore;

        [SerializeField] float _waitTime;

        [SerializeField] Text _timeText;
        [SerializeField] Text _goalText;

        [SerializeField] string _goalWord;
        [SerializeField] string _goalWord2;
        [SerializeField] string _goalWord3;

        [SerializeField] RectTransform _resultPanel;
        [SerializeField] Text _scoreText;
        [SerializeField] Text _timerText;
        [SerializeField] float _resultDuration = 0.2f;
        [SerializeField] float _textDuration = 0.1f;
        [SerializeField] float _tweenDelay = 0.5f;
        [SerializeField] Ease _textEase = Ease.OutQuint;
        [SerializeField] Image _resultIcon;
        [SerializeField] Sprite[] _icons;

        [SerializeField] float _diff = 3;

        string _timeStr;

        bool _isStop = false;
        bool _isFirst = false;
        bool _isEnded = false;

        void Update()
        {
            ScoreCalculation();
            if (_startTime >= LimitTime)
            {
                _isEnded = true;
                if (_isEnded)
                {
                    _timeText.text = "00" + ":" + "00" + ":" + "0" + (_startTime % 60).ToString("F3");
                    _timeStr = _timerText.text;
                }
            }
            if (!_isStop && !_isEnded)
            {
                CountTime();
                StopTime();
            }
        }

        void CountTime()
        {
            _startTime += Time.deltaTime;
            _timeText.text = "23" + ":" + "59" + ":" + _startTime.ToString("F3");
            _timeStr = _timeText.text;
            if (_startTime >= _firstActionTime && !_isFirst)
            {
                var seq = DOTween.Sequence();
                seq.Append(_timeText.DOFade(0f, 2f))
                    .OnComplete(ChangeBool);    
            }
        }
        //TODO;スコアを共有できるようにする
        void ScoreCalculation()
        {
            float diff = LimitTime - _startTime;

            _score = _baseScore - Math.Abs(diff * _diff);

            if (_startTime >= LimitTime + 1f)
            {
                _score = 0;
            }
            _scoreText.text = (_score*10).ToString("F0");
        }
        async void StopTime()
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
            {
                _isStop = true;
                _isEnded = true;

                Debug.Log(_startTime);
                Debug.Log(_score);

                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));

                ChangeIcon();

                _timerText.text = _timeStr;
                ApplicationManager.Instance.AddScore((int)_score);

                var seq = DOTween.Sequence();
                seq.Append(_resultPanel.DOScale(Vector3.one, _resultDuration).SetEase(Ease.OutBounce))
                   .Append(_timerText.DOFade(1f, _textDuration)).SetEase(_textEase)
                   .Append(_scoreText.DOFade(1f, _textDuration)).SetEase(_textEase)
                   .Append(_scoreText.DOCounter(0, (int)_score, _textDuration)).SetEase(_textEase)
                   .Append(_resultIcon.transform.DOScale(Vector3.one, _resultDuration)).SetEase(Ease.OutBounce);
            }

            if (_startTime >= LimitTime + 1f)
            {
                _isStop = true;
                _isEnded = true;

                ChangeIcon();

                _timerText.text = _timeStr;
                ApplicationManager.Instance.AddScore((int)_score);

                var seq = DOTween.Sequence();
                seq.Append(_resultPanel.DOScale(Vector3.one, _resultDuration).SetEase(Ease.OutBounce))
                   .Append(_timerText.DOFade(1f, _textDuration)).SetEase(_textEase)
                   .Append(_scoreText.DOFade(1f, _textDuration)).SetEase(_textEase)
                   .Append(_scoreText.DOCounter(0, (int)_score, _textDuration)).SetEase(_textEase)
                   .Append(_resultIcon.transform.DOScale(Vector3.one, _resultDuration)).SetEase(Ease.OutBounce);
            }
        }

        void ChangeIcon()
        {
            Sprite icon = null;
            var max = _baseScore * 10;
            float diff = LimitTime - _startTime;

            if (_score >= max * (4/5) && 0 >= diff)
            {
                icon = _icons[0];
            }
            else if(_score >= max * (1/2))
            {
                icon = _icons[1];
            }
            else
            {
                icon = _icons[2];
            }

            _resultIcon.sprite = icon;
        }

        void ShowText()
        {
            float diff = LimitTime - _startTime;
            if(diff < -_passTime)
            {
                _goalText.text = _goalWord3;
            }
            if(0 >= diff && diff >= -_passTime)
            {
                _goalText.text = _goalWord;
            }
            if(_passTime + 0.3f >= diff && diff > 0)
            {
                _goalText.text = _goalWord2;
            }
            if(diff > _passTime + 0.3f)
            {
                _goalText.text = _goalWord3;
            }
        }

        void ChangeBool()
        {
            _isFirst = true;
        }
    }
}



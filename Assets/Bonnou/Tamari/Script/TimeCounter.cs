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
        [SerializeField] Text _scoreText;
        [SerializeField] Text _goalText;

        [SerializeField] string _goalWord;
        [SerializeField] string _goalWord2;
        [SerializeField] string _goalWord3;

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
                }
            }
            if (!_isStop && !_isEnded)
            {
                CountTime();
                StopTime();
            }

            ShowText();
        }

        void CountTime()
        {
            _startTime += Time.deltaTime;
            _timeText.text = "23" + ":" + "59" + ":" + _startTime.ToString("F3");
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

            _score = _baseScore - Math.Abs(diff);

            if (_startTime >= LimitTime + 1f)
            {
                _score = 0;
            }
            _scoreText.text = (_score * 10).ToString("F0");
        }
        async void StopTime()
        {
            if (Input.GetButtonDown("Jump") && _isFirst || Input.GetButtonDown("Fire1") && _isFirst)
            {
                _isStop = true;
                _isEnded = true;

                Debug.Log(_startTime);
                Debug.Log(_score);
                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));

                var seq = DOTween.Sequence();
                seq.Append(_timeText.DOFade(1f, _waitTime))
                    .Append(_scoreText.DOFade(1f, _waitTime))
                    .Append(_goalText.DOFade(1f, _waitTime));
            }

            if (_startTime >= LimitTime + 1f)
            {
                _isStop = true;
                _isEnded = true;
                DOTween.Sequence()
                    .Append(_timeText.DOFade(1f, _waitTime))
                    .Join(_scoreText.DOFade(1f, _waitTime));
            }
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



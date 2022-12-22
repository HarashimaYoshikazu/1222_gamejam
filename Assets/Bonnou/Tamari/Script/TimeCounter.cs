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
        [SerializeField] float _limitTime;
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

        float _time2;
        bool _isStop;
        bool _isFirst;
        bool _isEnded;

        void Update()
        {
            ScoreCalculation();
            if (_startTime >= _limitTime)
            {
                _isEnded = true;
                if (_isEnded)
                {
                    _timeText.text = "00" + ":" + "00" + ":" + "0" + (_startTime % 60).ToString("F3");
                }
            }
            if (!_isStop)
            {
                CountTime();
                StopTime();
            }
        }

        void CountTime()
        {
            _startTime += Time.deltaTime;
            _timeText.text = "23" + ":" + "59" + ":" + _startTime.ToString("F3");
            if (_startTime >= _firstActionTime)
            {
                _isFirst = true;
                var seq = DOTween.Sequence();
                seq.Append(_timeText.DOFade(0f, 2f));
            }
        }
        //TODO;スコアを共有できるようにする
        void ScoreCalculation()
        {
            float diff = _limitTime - _startTime;

            _score = _baseScore - Math.Abs(diff);

            if (_startTime >= _limitTime + 1f)
            {
                _score = 0;
            }
            _scoreText.text = (_score * 10).ToString("F0");
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

                var seq = DOTween.Sequence();
                seq.Append(_timeText.DOFade(1f, 1f)).SetDelay(1f)
                    .Append(_scoreText.DOFade(1f, 1f))
                    .Append(_goalText.DOFade(1f, 0.5f));
            }

            if (_startTime >= _limitTime + 1f)
            {
                _isStop = true;
                _isEnded = true;
                DOTween.Sequence()
                    .Append(_timeText.DOFade(1f, 1f))
                    .Join(_scoreText.DOFade(1f, 1f));
            }
            ShowText();
        }

        void ShowText()
        {
            float diff = _limitTime - _startTime;
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
    }
}



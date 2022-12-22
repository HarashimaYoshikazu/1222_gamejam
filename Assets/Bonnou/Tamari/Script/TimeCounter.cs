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
        [SerializeField] float _limitTime;

        [SerializeField] float _firstActionTime;
        [SerializeField] float _secondActionTime;

        float _score;
        [SerializeField] float _baseScore;

        [SerializeField] float _waitTime;

        [SerializeField] Text _timeText;
        [SerializeField] Text _scoreText;
        [SerializeField] float _startTime;
        float _time2;
        bool _isStop;
        bool _isFirst;
        bool _isSecond;
        bool _isEnded;

        void Update()
        {
            ScoreCalculation();
            if (_startTime >= _limitTime)
            {
                _isEnded = true;
                if (_isEnded)
                {
                    _timeText.text = "00" + ":" + "00" + ":" + (_startTime % 60).ToString("F3");
                }
            }
            if (!_isStop)
            {
                CountTime();
                StopTime();
            }

            if (_isFirst)
            {

            }

            if (_isSecond)
            {
                
            }
        }

        void CountTime()
        {
            _startTime += Time.deltaTime;
            _timeText.text = "23" + ":" + "59" + ":" + _startTime.ToString("F3");
            if (_startTime >= _firstActionTime)
            {
                _isFirst = true;
            }
            if (_startTime >= _secondActionTime && !_isEnded)
            {
                _isSecond = true;
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
                seq.Append(_timeText.DOFade(1f, 1f));

                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));

                var seq2 = DOTween.Sequence();
                seq2.Append(_scoreText.DOFade(1f, 1f));
            }

            if (_startTime >= _limitTime + 1f)
            {
                _isStop = true;
                _isEnded = true;
                DOTween.Sequence()
                    .Append(_timeText.DOFade(1f, 1f))
                    .Join(_scoreText.DOFade(1f, 1f));
            }
        }
    }
}



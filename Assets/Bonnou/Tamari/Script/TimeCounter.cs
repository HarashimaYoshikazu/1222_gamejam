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
        [SerializeField] int _limitTime;

        [SerializeField] float _firstActionTime;
        [SerializeField] float _secondActionTime;

        float _score;
        [SerializeField] int _baseScore;

        [SerializeField] float _waitTime;

        [SerializeField] Text _timeText;
        [SerializeField] Text _scoreText;
        float _time;
        float _time2;
        bool _isStop;
        bool _isFirst;
        bool _isSecond;
        bool _isEnded;
        void Start()
        {
            _time = 40;
        }

        void Update()
        {
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
            ScoreCalculation();
        }

        void CountTime()
        {
            _time += Time.deltaTime;
            _timeText.text = "23" + ":" + "59" + ":" + _time.ToString("00");


            if (_time >= _firstActionTime)
            {
                _isFirst = true;
            }
            if (_time >= _secondActionTime)
            {
                _isSecond = true;
                var seq = DOTween.Sequence();
                seq.Append(_timeText.DOFade(0f, 1f));
            }

            else if (_time >= _limitTime)
            {
                _isEnded = true;
            }
        }
        void ScoreCalculation()
        {
            float diff = _limitTime - _time;

            _score = _baseScore - diff;

            _scoreText.text = _score.ToString();
        }
        async void StopTime()
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
            {
                _isStop = true;

                Debug.Log(_time);
                Debug.Log(_score);
                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));

                var seq = DOTween.Sequence();
                seq.Append(_timeText.DOFade(1f, 1f));

                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));

                var seq2 = DOTween.Sequence();
                seq2.Append(_scoreText.DOFade(1f, 1f));
            }

            if (_time > _limitTime + 1f)
            {
                _isStop = true;
                _score = 0;
                DOTween.Sequence()
                    .Append(_timeText.DOFade(1f, 1f))
                    .Join(_scoreText.DOFade(1f, 1f));
            }
        }
    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FukuMen
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private float _finishTimer = 20f;
        [SerializeField]
        private Text _timerText = null;
        [SerializeField]
        private Text _finishText = null;
        [SerializeField]
        private UnityEvent _finishEvent;

        [SerializeField]
        GameObject _selectPanel;

        public event Action OnGameFinish;

        private bool _start = false;

        private void Start()
        {
            Time.timeScale = 0;
            _start = false;
        }

        /// <summary>
        /// ボタンで呼び出す
        /// </summary>
        public void GameStart()
        {        
            _selectPanel.SetActive(false);
            Time.timeScale = 1;
            _timer = _finishTimer;
            _start = true;         
        }

        private void Update()
        {
            Timer();
        }

        private float _timer = 0;
        private void Timer()
        {
            if (_start == false) return;

            _timer -= Time.deltaTime;
            _timerText.text = _timer.ToString("00");
            if (0 >= _timer)
            {
                GameFinish("player");
                _start = false;
            }
        }

        private void GameFinish(string playerName)
        {
            _finishText.text = playerName + " 勝利！";
            if (OnGameFinish != null)
            {
                OnGameFinish();
            }

            _finishEvent!.Invoke();
        }
    }
}


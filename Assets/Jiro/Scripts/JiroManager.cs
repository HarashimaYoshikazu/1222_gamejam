using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Jiro
{
    public class JiroManager : MonoBehaviour
    {
        #region プロパティ
        public static JiroManager Instance;

        void Awake()
        {
            if(Instance)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }
        #endregion

        /// <summary>オーディオソースコンポーネント</summary>
        [SerializeField] AudioSource _audioSource;

        /// <summary>データのスクリプタブルオブジェクトたち</summary>
        [SerializeField] OrderDeta[] _orderDetas;

        /// <summary>お客さんの番号４人分</summary>
        int[] _customers = new int[4];

        /// <summary>現在のお客さんの番号</summary>
        int _currentCustomer = 0;

        /// <summary>ゲームのフェイズ</summary>
        GamePhase _gamePhase = GamePhase.GameReady;

        /// <summary>コールのクリップ小、小豚、大</summary>
        [SerializeField] AudioClip[] _callClips;

        JiroValue _currentNinniku = JiroValue.Default;
        JiroValue _currentYasai = JiroValue.Default;
        JiroValue _currentAbura = JiroValue.Default;
        JiroValue _currentKarame = JiroValue.Default;

        bool _currentSelect;

        /// <summary>
        /// ゲームスタートボタン
        /// </summary>
        public void GameStart()
        {
            RandomSelectCustmers();
            //アニメーション流す
            //↓アニメーションイベントがいい
            Call();
        }

        /// <summary>
        /// お客さんをランダムに決める
        /// </summary>
        void RandomSelectCustmers()
        {
            for(int i = 0; i < _customers.Length; i++)
            {
                _customers[i] = UnityEngine.Random.Range(0, _orderDetas.Length);
            }
        }

        public void Call()
        {
            StartCoroutine(CallAsync());
        }

        IEnumerator CallAsync()
        {
            switch(_orderDetas[_customers[_currentCustomer]].JiroType)
            {
                case JiroType.Syou:
                    _audioSource.PlayOneShot(_callClips[0]);
                    break;
                case JiroType.SyouButa:
                    _audioSource.PlayOneShot(_callClips[1]);
                    break;
                case JiroType.Dai:
                    _audioSource.PlayOneShot(_callClips[2]);
                    break;
            }
            yield return 1.5f;
            _audioSource.PlayOneShot(_orderDetas[_customers[_currentCustomer]].Call);
            _gamePhase = GamePhase.InGame;
        }

        public void LastSelect()
        {
            if(Judge())
            {
                Debug.Log("成功です");
            }
            else
            {
                Debug.Log("失敗です");
            }
            _gamePhase = GamePhase.GameReady;
            //アニメーター流す
            //↓アニメーションイベントがいい
            NextCustmor();
            Call();
        }

        bool Judge()
        {
            if(_orderDetas[_customers[_currentCustomer]].Ninniku != _currentNinniku)
                return false;
            if (_orderDetas[_customers[_currentCustomer]].Yasai != _currentYasai)
                return false;
            if (_orderDetas[_customers[_currentCustomer]].Abura != _currentAbura)
                return false;
            if (_orderDetas[_customers[_currentCustomer]].Karame != _currentKarame)
                return false;
            return true;
        }

        public void Open(GameObject go)
        {
            if (_currentSelect || _gamePhase == GamePhase.GameReady)
                return;
            go.SetActive(true);
            _currentSelect = true;
        }

        public void Close(GameObject go)
        {
            go.SetActive(false);
            _currentSelect = false;
        }

        public void NinnikuChanege(int jiroValue)
        {
            _currentNinniku = (JiroValue)jiroValue;
        }

        public void YasaiChanege(int jiroValue)
        {
            _currentYasai = (JiroValue)jiroValue;
        }

        public void AburaChange(int jiroValue)
        {
            _currentAbura = (JiroValue)jiroValue;
        }

        public void KarameChange(int jiroValue)
        {
            _currentKarame = (JiroValue)jiroValue;
        }

        void NextCustmor()
        {
            if(_currentCustomer == 3)
            {
                GameFinish();
            }
            _currentCustomer++;
        }

        void GameFinish()
        {
            Debug.Log("二郎終了");
        }

        enum GamePhase
        {
            GameReady,
            InGame,
        }

        /// <summary>
        /// トッピングの量
        /// </summary>
        public enum JiroValue
        {
            Small,
            Default,
            Mashi,
        }

        /// <summary>
        /// 二郎の種類
        /// </summary>
        public enum JiroType
        {
            Syou,
            SyouButa,
            Dai
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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

        JiroValue _currentNinniku = JiroValue.ふつう;
        JiroValue _currentYasai = JiroValue.ふつう;
        JiroValue _currentAbura = JiroValue.ふつう;
        JiroValue _currentKarame = JiroValue.ふつう;

        bool _currentSelect;

        [SerializeField] GameObject _startPanel;

        [SerializeField] Text _ninnikuText;
        [SerializeField] Text _yasaiText;
        [SerializeField] Text _aburaText;
        [SerializeField] Text _karameText;
        [SerializeField] Animator _animator;
        [SerializeField] GameObject[] _hudas;
        [SerializeField] GameObject[] _hudaPos;

        [SerializeField] Image[] _customerImages;

        [SerializeField] AudioClip _finishClip;

        [SerializeField] AudioClip _collectClip;
        [SerializeField] AudioClip _failedClip;
        int _currentScore;

        void Update()
        {
            _ninnikuText.text = "にんにく:"+_currentNinniku.ToString();
            _yasaiText.text = "やさい:" + _currentYasai.ToString();
            _aburaText.text = "あぶら:" + _currentAbura.ToString();
            _karameText.text = "からめ:" + _currentKarame.ToString();
        }

        /// <summary>
        /// ゲームスタートボタン
        /// </summary>
        public void GameStart()
        {
            ResetValue();
            _startPanel.SetActive(false);
            RandomSelectCustmers();
            _animator.Play("Coming");
            //↓アニメーションイベントでよぶ
            //Call();
        }

        /// <summary>
        /// お客さんをランダムに決める
        /// </summary>
        void RandomSelectCustmers()
        {
            for (int i = 0; i < _customers.Length; i++)
            {
                _customers[i] = UnityEngine.Random.Range(0, _orderDetas.Length);
                Instantiate(_hudas[(int)_orderDetas[_customers[i]].JiroType], _hudaPos[i].transform);
            }
        }

        public void Call()
        {
            Debug.Log($"{_orderDetas[_customers[_currentCustomer]].Ninniku},{_orderDetas[_customers[_currentCustomer]].Yasai}," +
                $"{_orderDetas[_customers[_currentCustomer]].Abura},{_orderDetas[_customers[_currentCustomer]].Karame}");
            StartCoroutine(CallAsync());
        }

        IEnumerator CallAsync()
        {
            switch (_orderDetas[_customers[_currentCustomer]].JiroType)
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
            yield return new WaitForSeconds(3f);
            _audioSource.pitch = UnityEngine.Random.Range(0.8f, 2.0f);
            _audioSource.PlayOneShot(_orderDetas[_customers[_currentCustomer]].Call);
            _gamePhase = GamePhase.InGame;
        }

        public void LastSelect()
        {
            _audioSource.pitch = 1;
            if (Judge())
            {
                //見た目変える
                Debug.Log("成功です");
                _customerImages[_currentCustomer].color = Color.red;
                _audioSource.PlayOneShot(_collectClip);
                _currentScore += 150;
            }
            else
            {
                _customerImages[_currentCustomer].color = Color.blue;
                _audioSource.PlayOneShot(_failedClip);
                Debug.Log("失敗です");
            }
            _gamePhase = GamePhase.GameReady;
            StartCoroutine(Stay());
        }

        IEnumerator Stay()
        {
            yield return new WaitForSeconds(1);
            NextCustmor();
            if (_currentCustomer == 4)
            {
                GameFinish();
            }
            if(_gamePhase != GamePhase.GameEnd)
            {
                Call();
            }
        }

        bool Judge()
        {
            if (_orderDetas[_customers[_currentCustomer]].Ninniku != _currentNinniku)
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
            ResetValue();
            _currentCustomer++;
        }

        void GameFinish()
        {
            Instance = null;
            _audioSource.PlayOneShot(_finishClip);
            _gamePhase = GamePhase.GameEnd;
            ApplicationManager.Instance.AddScore(_currentScore);
            ApplicationManager.Instance.RandomSceneChange();
            Debug.Log("二郎終了");
        }

        void ResetValue()
        {
            _currentNinniku = JiroValue.ふつう;
            _currentYasai = JiroValue.ふつう;
            _currentAbura = JiroValue.ふつう;
            _currentKarame = JiroValue.ふつう;
        }

        enum GamePhase
        {
            GameReady,
            InGame,
            GameEnd
        }

        /// <summary>
        /// トッピングの量
        /// </summary>
        public enum JiroValue
        {
            すくなめ,
            ふつう,
            まし,
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
